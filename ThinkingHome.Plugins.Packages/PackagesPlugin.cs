using System.Collections.Generic;
using System.Diagnostics;
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

		private static object BuildModel(HomePackageInfo packageInfo)
		{
			if (packageInfo == null)
			{
				return null;
			}

			return new
			{
				id = packageInfo.PackageId,
				version = packageInfo.PackageVersion,
				description = packageInfo.PackageDescription,
				installedVersion = packageInfo.InstalledVersion
			};
		}

		[HttpCommand("/api/packages/list")]
		public object GetPackages(HttpRequestParams request)
		{
			string query = request.GetString("query");

			var list = Context.PackageManager.GetPackages(query);

			return list.Select(BuildModel).Where(x => x != null).ToArray();
		}

		[HttpCommand("/api/packages/installed")]
		public object GetInstalledPackages(HttpRequestParams request)
		{
			var list = Context.PackageManager.GetInstalledPackages();
			return list.Select(BuildModel).Where(x => x != null).ToArray();
		}

		[HttpCommand("/api/packages/install")]
		public object Install(HttpRequestParams request)
		{
			string packageId = request.GetRequiredString("packageId");

			var package = Context.PackageManager.Install(packageId);
			return BuildModel(package);
		}

		[HttpCommand("/api/packages/update")]
		public object Update(HttpRequestParams request)
		{
			string packageId = request.GetRequiredString("packageId");

			var package = Context.PackageManager.Update(packageId);
			return BuildModel(package);
		}

		[HttpCommand("/api/packages/uninstall")]
		public object UnInstall(HttpRequestParams request)
		{
			string packageId = request.GetRequiredString("packageId");

			var package = Context.PackageManager.UnInstall(packageId);
			return BuildModel(package);
		}

		#endregion
	}
}
