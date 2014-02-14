using System;
using Microsoft.Deployment.WindowsInstaller;
using UWS.Configuration;

namespace ThinkingHome.SetupCustomActions
{
	public class CustomActions
	{
		[CustomAction("RegisterApplication")]
		public static ActionResult RegisterApplication(Session session)
		{
			session.Log("Begin configure web application");

			try
			{
				// parameters
				var appId = new Guid(session["APP_ID"]);
				ushort port = ushort.Parse(session["APP_PORT"]);
				string path = session["APP_PATH"];

				// init
				WebAppConfigEntry app = Metabase.GetWebAppEntry(appId);
				app.ApplicationName = "NooLite Web Control Panel";
				app.VirtualDirectory = string.Empty;
				app.PhysicalDirectory = path;

				app.ListenAddresses.Clear();
				app.ListenAddresses.AddAddresses(new ListenAddress(port));

				app.AppType = ApplicationType.AspNetOrStaticHtml;
				app.Stopped = false;

				// register webapp
				Metabase.RegisterApplication(RuntimeVersion.AspNet_4, false, app, new AppShortcut[0]);
			}
			catch (Exception ex)
			{
				session.Log("ERROR in configure web application: {0}", ex.ToString());
				return ActionResult.Failure;
			}

			session.Log("End configure web application");
			return ActionResult.Success;
		}

		[CustomAction("UnRegisterApplication")]
		public static ActionResult UnRegisterApplication(Session session)
		{
			session.Log("Begin unregister web application");

			try
			{
				var appId = new Guid(session["APP_ID"]);
				Metabase.UnregisterApplication(appId);
			}
			catch (Exception ex)
			{
				session.Log("ERROR in unregister web application: {0}", ex.ToString());
				return ActionResult.Failure;
			}

			session.Log("End unregister web application");
			return ActionResult.Success;
		}

		[CustomAction("CheckWebServer")]
		public static ActionResult CheckWebServer(Session session)
		{
			try
			{
				Metabase.GetRegisteredApplicationCount();
				session["SERVER_IS_INSTALLED"] = "1";
			}
			catch (Exception ex)
			{
				session["SERVER_IS_INSTALLED"] = "0";
			}

			return ActionResult.Success;
		}
	}
}
