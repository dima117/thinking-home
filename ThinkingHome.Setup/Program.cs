using System;
using WixSharp;
using WixSharp.CommonTasks;
using Microsoft.Deployment.WindowsInstaller;

namespace ThinkingHome.Setup
{
	class Program
	{
		static void Main(string[] args)
		{
			var project = new Project("Thinking Home", 
				new Dir(@"%ProgramFiles%\ThinkingHome", new Files("*.*")),
				new ElevatedManagedAction("InstallService", Return.check, When.After, Step.InstallFiles, Condition.NOT_Installed),
				new ElevatedManagedAction("UnInstallService", Return.check, When.Before, Step.RemoveFiles, Condition.BeingRemoved))
			{
				SourceBaseDir = @"C:\Users\dima117\Documents\Visual Studio 2015\Projects\thinking-home\Setup\dist\service",
                LicenceFile = "License.rtf",
                Platform = Platform.x64,
				GUID = new Guid("25F88AAD-CF60-4CA4-ADD3-F01A4F0F0313")
			};

			Compiler.BuildMsi(project);
		}
	}

	public class CustomActions
	{
		[CustomAction]
		public static ActionResult InstallService(Session session)
		{
			return session.HandleErrors(() =>
			{
				Tasks.InstallService(session.Property("INSTALLDIR") + "ThinkingHome.Service.exe", true);
				Tasks.StartService("ThinkingHome.Service", false);
			});
		}

		[CustomAction]
		public static ActionResult UnInstallService(Session session)
		{
			return session.HandleErrors(() =>
			{
				Tasks.InstallService(session.Property("INSTALLDIR") + "ThinkingHome.Service.exe", false);
			});
		}
	}
}
