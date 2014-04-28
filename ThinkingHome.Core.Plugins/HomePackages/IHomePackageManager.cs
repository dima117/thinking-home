using System.Collections.Generic;

namespace ThinkingHome.Core.Plugins.HomePackages
{
	public interface IHomePackageManager
	{
		List<HomePackageInfo> GetPackages(string name);
		List<HomePackageInfo> GetInstalledPackages();
		HomePackageInfo Install(string packageId);
		HomePackageInfo UnInstall(string packageId);
		HomePackageInfo Update(string packageId);
	}
}