using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using NHibernate;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Core.Plugins.HomePackages;

namespace ThinkingHome.Core.Infrastructure
{
	[Export(typeof(IServiceContext))]
	public class ServiceContext : IServiceContext
	{
		#region plugins

		// todo: переопределить равенство - сравнивать по типу
		[ImportMany(typeof(PluginBase))]
		protected HashSet<PluginBase> Plugins { get; set; }

		public IReadOnlyCollection<PluginBase> GetAllPlugins()
		{
			return new ReadOnlyCollection<PluginBase>(Plugins.ToList());
		}

		public T GetPlugin<T>() where T : PluginBase
		{
			return Plugins.FirstOrDefault(p => p is T) as T;
		}

		#endregion 

		[Import(typeof(IHomePackageManager))]
		public IHomePackageManager PackageManager { get; protected set; }

		#region data

		private ISessionFactory sessionFactory;

		public void InitSessionFactory(ISessionFactory sessionFactory)
		{
			this.sessionFactory = sessionFactory;
		}

		public ISession OpenSession()
		{
			return sessionFactory.OpenSession();
		}

		public IStatelessSession OpenStatelessSession()
		{
			return sessionFactory.OpenStatelessSession();
		}

		#endregion
	}
}
