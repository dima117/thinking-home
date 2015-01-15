using System;
using ThinkingHome.Core.Infrastructure;
using ThinkingHome.Plugins.OneWire;
using ThinkingHome.Plugins.OneWire.Sensors;

namespace ThinkingHome.TestConsole
{
	internal class Program
	{
		private static void Main()
		{
			//TestServer();
            TestOneWire();
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
        
        private static void TestOneWire()
        {
            var plugin = new OneWirePluggin();

            plugin.InitPlugin();

            plugin.StartPlugin();

            var devices = plugin.GetDevices();

            foreach(var d in devices)
            {
                Console.WriteLine("{0} - {1}", d.DeviceName, d.DeviceType);

                var tpr = d as TemperatureSensorBase;
                if (tpr != null)
                {
                    Console.WriteLine("Temperature: {0}", tpr.GetTemperature());
                }
            }

            Console.ReadLine();

            plugin.StopPlugin();
        }
	}
}
