using System.Linq;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Core.Plugins.HomePackages;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.Packages.Lang;
using ThinkingHome.Plugins.WebUI.Attributes;

namespace ThinkingHome.Plugins.Packages
{
	[AppSection("Installed packages", 
		SectionType.System, "/webapp/packages/list.js", "ThinkingHome.Plugins.Packages.Resources.list.js",
		LangResourceType = typeof(PackagesLang), LangResourceKey = "Installed_packages")]

	[JavaScriptResource("/webapp/packages/list-model.js", "ThinkingHome.Plugins.Packages.Resources.list-model.js")]
	[JavaScriptResource("/webapp/packages/list-view.js", "ThinkingHome.Plugins.Packages.Resources.list-view.js")]
	[HttpEmbeddedResource("/webapp/packages/list-item.tpl", "ThinkingHome.Plugins.Packages.Resources.list-item.tpl")]
	[HttpEmbeddedResource("/webapp/packages/list.tpl", "ThinkingHome.Plugins.Packages.Resources.list.tpl")]

	// i18n
	[HttpI18NResource("/webapp/packages/lang.json", "ThinkingHome.Plugins.Packages.Lang.PackagesLang")]

	[Plugin]
	public class PackagesPlugin : PluginBase
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
