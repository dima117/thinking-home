using System;
using System.Collections.Generic;
using NHibernate;
using ThinkingHome.Core.Plugins.HomePackages;

namespace ThinkingHome.Core.Plugins
{
	public interface IServiceContext
	{
		IHomePackageManager PackageManager { get; }

		IReadOnlyCollection<Plugin> GetAllPlugins();

		T GetPlugin<T>() where T : Plugin;

		ISession OpenSession();
	}
}
