using System.Linq;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Core.Plugins.HomePackages;
using ThinkingHome.Plugins.Listener;
using ThinkingHome.Plugins.Listener.Api;

namespace ThinkingHome.Plugins.Packages
{
	[Plugin]
	public class PackagesPlugin : Plugin
	{
		[HttpCommand("/api/packages/list")]
		public object GetPackages(HttpRequestParams request)
		{
			string query = request.GetString("query");

			return Context.PackageManager.GetPackages(query)
				.Select(BuildModel)
				.ToArray();
		}

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
	}
}
