using System;
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
			HomeEnvironment.Init();
			
			var app = new HomeApplication();

			app.Init();
			app.StartServices();

			Console.WriteLine("Service is available. Press ENTER to exit.");
			Console.ReadLine();

			app.StopServices();
		}
	}
}
