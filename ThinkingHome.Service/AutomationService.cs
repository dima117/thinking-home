using System;
using System.ServiceProcess;
using System.Threading.Tasks;
using ThinkingHome.Core.Infrastructure;

namespace ThinkingHome.Service
{
	public partial class AutomationService : ServiceBase
	{
		private Task<HomeApplication> appTask;

		public AutomationService()
		{
			InitializeComponent();
			HomeEnvironment.Init();
		}

		protected override void OnStart(string[] args)
		{
			appTask = Task.Run(() => {
				var app = new HomeApplication();
				app.Init();
				app.StartServices();

				return app;
			});
        }

		protected override void OnStop()
		{
			appTask.Wait();
			appTask.Result.StopServices();
		}
	}
}
