using System.IO;
using System.ServiceProcess;
using ThinkingHome.Core.Infrastructure;

namespace ThinkingHome.Service
{
	public partial class AutomationService : ServiceBase
	{
		private readonly HomeApplication app;

		public AutomationService()
		{
			InitializeComponent();

			var path = typeof(Program).Assembly.Location;
			var currentDirectory = Path.GetDirectoryName(path);
			Directory.SetCurrentDirectory(currentDirectory);

			app = new HomeApplication();
			app.Init();
		}

		protected override void OnStart(string[] args)
		{
			app.StartServices();
		}

		protected override void OnStop()
		{
			app.StopServices();
		}
	}
}
