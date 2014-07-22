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

			HomeEnvironment.Init();

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
