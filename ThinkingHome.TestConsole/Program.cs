using System;
using System.IO;
using ThinkingHome.Core.Infrastructure;

namespace ThinkingHome.TestConsole
{
	internal class Program
	{
		private static void Main()
		{
			TestServer();
		}
		
		private static void TestServer()
		{
			var path = typeof (Program).Assembly.Location;
			var currentDirectory = Path.GetDirectoryName(path);
			Directory.SetCurrentDirectory(currentDirectory);

			var app = new HomeApplication();

			app.Init();
			app.StartServices();

			Console.WriteLine("Service is available. Press ENTER to exit.");
			Console.ReadLine();

			app.StopServices();
		}
	}
}
