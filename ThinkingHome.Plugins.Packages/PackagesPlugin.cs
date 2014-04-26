using System.Linq;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Core.Plugins.HomePackages;
using ThinkingHome.Plugins.Listener;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Handlers;
using ThinkingHome.Plugins.WebUI.Attributes;

namespace ThinkingHome.Plugins.Packages
{
	[AppSection("Packages", "/webapp/packages/list-controller.js", "ThinkingHome.Plugins.Packages.PluginResources.list-controller.js")]
	[JavaScriptResource("/webapp/packages/list-model.js", "ThinkingHome.Plugins.Packages.PluginResources.list-model.js")]
	[JavaScriptResource("/webapp/packages/list-view.js", "ThinkingHome.Plugins.Packages.PluginResources.list-view.js")]
	[HttpResource("/webapp/packages/list.tpl", "ThinkingHome.Plugins.Packages.PluginResources.list.tpl")]
	[HttpResource("/webapp/packages/list-item.tpl", "ThinkingHome.Plugins.Packages.PluginResources.list-item.tpl")]

	[Plugin]
	public class PackagesPlugin : Plugin
	{
		#region api

		private static object BuildModel(HomePackageInfo x)
		{
			return new
			{
				id = x.PackageId,
				version = x.PackageVersion,
				description = x.PackageDescription,
				installedVersion = x.InstalledVersion
			};
		}

		[HttpCommand("/api/packages/list")]
		public object GetPackages(HttpRequestParams request)
		{
			
			string query = request.GetString("query");

			return Context.PackageManager.GetPackages(query)
				.Select(BuildModel)
				.ToArray();
		}

		[HttpCommand("/api/packages/installed")]
		public object GetInstalledPackages(HttpRequestParams request)
		{
			return Context.PackageManager.GetInstalledPackages();
		}

		[HttpCommand("/api/packages/install")]
		public object Install(HttpRequestParams request)
		{
			string packageId = request.GetRequiredString("packageId");

			Context.PackageManager.Install(packageId);

			return null;
		}

		[HttpCommand("/api/packages/update")]
		public object Update(HttpRequestParams request)
		{
			string packageId = request.GetRequiredString("packageId");

			Context.PackageManager.Update(packageId);
			return null;
		}

		[HttpCommand("/api/packages/uninstall")]
		public object UnInstall(HttpRequestParams request)
		{
			string packageId = request.GetRequiredString("packageId");

			Context.PackageManager.UnInstall(packageId);
			return null;
		}

		#endregion
	}
}
