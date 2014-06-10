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
		[ImportMany(typeof(Plugin))]
		protected HashSet<Plugin> Plugins { get; set; }

		public IReadOnlyCollection<Plugin> GetAllPlugins()
		{
			return new ReadOnlyCollection<Plugin>(Plugins.ToList());
		}

		public T GetPlugin<T>() where T : Plugin
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

		#endregion
	}
}
