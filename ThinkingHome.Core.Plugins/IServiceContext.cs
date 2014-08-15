using System;
using System.Collections.Generic;
using NHibernate;
using ThinkingHome.Core.Plugins.HomePackages;

namespace ThinkingHome.Core.Plugins
{
	public interface IServiceContext
	{
		IHomePackageManager PackageManager { get; }

		IReadOnlyCollection<PluginBase> GetAllPlugins();

		T GetPlugin<T>() where T : PluginBase;

		ISession OpenSession();
	}
}
