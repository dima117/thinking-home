using System.Collections.Generic;

namespace ThinkingHome.Core.Plugins.HomePackages
{
	public interface IHomePackageManager
	{
		List<HomePackageInfo> GetPackages(string name);
		List<HomePackageInfo> GetInstalledPackages();
		void Install(string packageId);
		void UnInstall(string packageId);
		void Update(string packageId);
	}
}