using System.Linq;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Core.Plugins.HomePackages;
using ThinkingHome.Plugins.Listener;

namespace ThinkingHome.Plugins.Packages
{
	[Plugin]
	public class PackagesPlugin : Plugin
	{
		[ExtCommand("Packages", "GetPackages")]
		public object GetPackages(dynamic args)
		{
			string query = args.query;

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

		[ExtCommand("Packages", "GetInstalledPackages")]
		public object GetInstalledPackages(dynamic args)
		{
			return Context.PackageManager.GetInstalledPackages();
		}

		[ExtCommand("Packages", "Install")]
		public object Install(dynamic args)
		{
			string packageId = args.packageId;

			Context.PackageManager.Install(packageId);

			return null;
		}

		[ExtCommand("Packages", "Update")]
		public object Update(dynamic args)
		{
			string packageId = args.packageId;

			Context.PackageManager.Update(packageId);
			return null;
		}

		[ExtCommand("Packages", "UnInstall")]
		public object UnInstall(dynamic args)
		{
			string packageId = args.packageId;

			Context.PackageManager.UnInstall(packageId);
			return null;
		}
	}
}
