using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using ECM7.Migrator;
using ECM7.Migrator.Framework;
using ECM7.Migrator.Providers;
using ECM7.Migrator.Providers.SqlServer;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NLog;
using ThinkingHome.Core.Plugins;

namespace ThinkingHome.Core.Infrastructure
{
	public class HomeApplication
	{
		private readonly Logger logger = LogManager.GetCurrentClassLogger();

		[Import(typeof(IServiceContext))]
		private ServiceContext context;

		public HomeApplication()
		{
			AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
		}

		private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();

			var assembly = assemblies.FirstOrDefault(a =>
			{
				var assemblyName = a.GetName();
				return assemblyName.Name == args.Name || assemblyName.FullName == args.Name;
			});

			return assembly;
		}

		private static void InitSessionFactory(ServiceContext context)
		{
			var cfg = new Configuration();

			var mapper = new ConventionModelMapper();


			mapper.BeforeMapClass +=
				(inspector, type, map) =>
				{
					var idProperty = type.GetProperty("Id");
					map.Id(idProperty, idMapper => { });
				};

			mapper.BeforeMapProperty +=
				(inspector, propertyPath, map) => map.Column(propertyPath.ToColumnName());

			mapper.BeforeMapManyToOne +=
				(inspector, propertyPath, map) => map.Column(propertyPath.ToColumnName() + "Id");

			foreach (var plugin in context.GetAllPlugins())
			{
				plugin.InitDbModel(mapper);
				cfg.AddAssembly(plugin.GetType().Assembly);
			}

			var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();


			cfg.DataBaseIntegration(dbConfig =>
			{
				dbConfig.Dialect<MsSqlCe40Dialect>();
				dbConfig.Driver<SqlServerCeDriver>();
				dbConfig.ConnectionStringName = "common";
			});

			cfg.AddDeserializedMapping(mapping, null);	//Loads nhibernate mappings

			var sessionFactory = cfg.BuildSessionFactory();
			context.InitSessionFactory(sessionFactory);
		}

		public void Init()
		{
			try
			{
				ShadowCopyPlugins();

				LoadPlugins();

				InitSessionFactory(context);

				// обновляем структуру БД
				using (var session = context.OpenSession())
				{
					foreach (var plugin in context.GetAllPlugins())
					{
						UpdateDatabase(session.Connection, plugin);
					}
				}

				// инициализируем плагины
				foreach (var plugin in context.GetAllPlugins())
				{
					logger.Info("init plugin: {0}", plugin.GetType().FullName);
					plugin.Init();
				}
			}
			catch (Exception ex)
			{
				logger.ErrorException("error on plugins initialization", ex);
				throw;
			}
		}

		public void StartServices()
		{
			try
			{
				foreach (var plugin in context.GetAllPlugins())
				{
					logger.Info("start plugin {0}", plugin.GetType().FullName);
					plugin.Start();
				}

				logger.Info("all plugins running");
			}
			catch (Exception ex)
			{
				logger.ErrorException("error on start plugins", ex);
				throw;
			}
		}

		public void StopServices()
		{
			foreach (var plugin in context.GetAllPlugins())
			{
				try
				{
					logger.Info("stop plugin {0}", plugin.GetType().FullName);
					plugin.Stop();
				}
				catch (Exception ex)
				{
					logger.ErrorException("error on stop plugins", ex);
				}
			}

			logger.Info("all plugins are stopped");
		}

		#region private

		private void LoadPlugins()
		{
			logger.Info("load plugins");

			var folders = new HashSet<string>();

			var catalog = new AggregateCatalog(new ApplicationCatalog());

			var spDir = new DirectoryInfo(AppSettings.ShadowedPluginsFullPath);

			foreach (var dir in spDir.GetDirectories())
			{
				var subCatalog = new DirectoryCatalog(dir.FullName);
				catalog.Catalogs.Add(subCatalog);

				folders.Add(dir.FullName);
			}

			AppDomain.CurrentDomain.SetupInformation.PrivateBinPath = string.Join(";", folders);

			var container = new CompositionContainer(catalog);
			container.SatisfyImportsOnce(this);
		}

		private void UpdateDatabase(IDbConnection connection, Plugin plugin)
		{
			var assembly = plugin.GetType().Assembly;

			logger.Info("update database: {0}", assembly.FullName);

			// todo: sql
			var provider = ProviderFactory.Create<SqlServerCeTransformationProvider>(connection, null);
			//var provider = ProviderFactory.Create<SqlServerTransformationProvider>(connection, null);

			using (var migrator = new Migrator(provider, assembly))
			{
				// запрещаем выполнять миграции, для которых не указано "пространство имен"
				if (migrator.AvailableMigrations.Any())
				{
					var migrationsInfo = assembly.GetCustomAttribute<MigrationAssemblyAttribute>();

					if (migrationsInfo == null || string.IsNullOrWhiteSpace(migrationsInfo.Key))
					{
						logger.Error("assembly {0} contains invalid migration info", assembly.FullName);
					}
				}

				migrator.Migrate();
			}
		}

		public void ShadowCopyPlugins()
		{
			logger.Info("shadow copy plugins");

			var shadowedPlugins = new DirectoryInfo(AppSettings.ShadowedPluginsFullPath);

			if (shadowedPlugins.Exists)
			{
				shadowedPlugins.Delete(true);
			}

			shadowedPlugins.Create();

			// Shadow copy plugins (avoid the CLR locking DLLs)
			var plugins = new DirectoryInfo(AppSettings.PluginsFullPath);

			if (!plugins.Exists)
			{
				plugins.Create();
			}

			CopyTo(plugins, shadowedPlugins);
		}

		private static void CopyTo(DirectoryInfo from, DirectoryInfo to)
		{
			foreach (FileInfo file in from.GetFiles())
			{
				string temppath = Path.Combine(to.FullName, file.Name);
				file.CopyTo(temppath, true);
			}

			foreach (DirectoryInfo dir in from.GetDirectories())
			{
				var subdir = to.CreateSubdirectory(dir.Name);
				CopyTo(dir, subdir);
			}
		}

		#endregion
	}
}
