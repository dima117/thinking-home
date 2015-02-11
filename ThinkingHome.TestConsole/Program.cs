using System;
using ThinkingHome.Core.Infrastructure;
//using ThinkingHome.Plugins.OneWire;
//using ThinkingHome.Plugins.OneWire.Sensors;
//using System.Linq;

namespace ThinkingHome.TestConsole
{
	internal class Program
	{
		private static void Main()
		{
			TestServer();
			//TestOneWire();
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
            /*var plugin = new OneWirePluggin();

            plugin.InitPlugin();

            plugin.StartPlugin();

            var devices = plugin.GetDevices();

            Console.WriteLine("Find sensors:\r\n");

            foreach (var d in devices)
            {
                Console.WriteLine("{0} - {1}", d.DeviceName, d.DeviceType);
            }
            
            Console.WriteLine("\r\nTemperature sensors 5 iterations:\r\n");
            
            var temperatureSesors = devices.Where(p => p is TemperatureSensorBase)
                                           .Cast<TemperatureSensorBase>();

            for (int i = 0; i < 5; i++)
            {
                foreach (var tpr in temperatureSesors)
                {
                    Console.WriteLine("Temperature: {0}", tpr.GetTemperature());
                }
            }
            

            Console.ReadLine();

            plugin.StopPlugin();*/
        }
	}
}
