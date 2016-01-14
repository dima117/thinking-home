using System.Configuration.Install;
using System.ComponentModel;
using System.ServiceProcess;

namespace ThinkingHome.Service
{
	[RunInstaller(true)]
	public class AutomationServiceInstaller : Installer
	{
		public AutomationServiceInstaller()
		{
			InitializeComponent();
			serviceInstaller.StartType = ServiceStartMode.Automatic;
			serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
		}

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}

			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			serviceProcessInstaller = new ServiceProcessInstaller();
			serviceInstaller = new ServiceInstaller();

			// 
			// serviceProcessInstaller
			// 
			serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
			serviceProcessInstaller.Password = null;
			serviceProcessInstaller.Username = null;

			// 
			// serviceInstaller1
			// 
			serviceInstaller.ServiceName = "ThinkingHome.Service";
			serviceInstaller.DisplayName = "Home automation service";
			serviceInstaller.Description = "A service that does very interesting things";

			// 
			// ProjectInstaller
			// 
			Installers.AddRange(new Installer[] { serviceProcessInstaller, serviceInstaller});
		}

		#endregion Component Designer generated code

		private ServiceProcessInstaller serviceProcessInstaller;
		private ServiceInstaller serviceInstaller;
	}
}
