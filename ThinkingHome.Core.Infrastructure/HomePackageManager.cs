using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using NuGet;
using ThinkingHome.Core.Plugins.Packages;

namespace ThinkingHome.Core.Infrastructure
{
	[Export(typeof(IHomePackageManager))]
	public class HomePackageManager : IHomePackageManager
	{
		private readonly PackageManager pManager;

		public HomePackageManager()
		{
			var repository = PackageRepositoryFactory.Default
				.CreateRepository(AppSettings.PluginsRepository);

			pManager = new PackageManager(repository, AppSettings.PluginsFolder);
		}

		public List<HomePackageInfo> GetPackages(string name)
		{
			var query = pManager.SourceRepository.GetPackages();

			if (!string.IsNullOrWhiteSpace(name))
			{
				query = query.Where(p => p.GetFullName().Contains(name));
			}

			var packages = query.OrderBy(p => p.Id).ToList();

			var model = packages.Select(MapPackageInfo).ToList();

			return model;
		}

		public List<HomePackageInfo> GetInstalledPackages()
		{
			var packages = pManager.LocalRepository
				.GetPackages()
				.OrderBy(p => p.Id)
				.ToList();

			var model = packages.Select(MapPackageInfo).ToList();

			return model;
		}

		public HomePackageInfo MapPackageInfo(IPackage p)
		{
			IPackage dummy = pManager.LocalRepository.FindPackage(p.Id);

			var installedVersion = dummy == null ? null : dummy.Version.ToString();

			return new HomePackageInfo
			{
				PackageId = p.Id,
				PackageVersion = p.Version.ToString(),
				PackageDescription = p.Description,
				InstalledVersion = installedVersion
			};
		}


		#region installation

		public void Install(string packageId)
		{
			pManager.InstallPackage(packageId);
		}

		public void UnInstall(string packageId)
		{
			pManager.UninstallPackage(packageId);
		}

		public void Update(string packageId)
		{
			pManager.UpdatePackage(packageId, true, false);
		}

		#endregion
	}
}
