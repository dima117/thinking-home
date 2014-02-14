using System.ServiceProcess;

namespace ThinkingHome.Service
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main()
		{
			var service = new AutomationService();
			ServiceBase.Run(service);
		}
	}
}
