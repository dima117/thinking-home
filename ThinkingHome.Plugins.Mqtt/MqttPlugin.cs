using System;
using System.Configuration;
using System.Diagnostics;
using System.Text;
using ECM7.Migrator.Framework;
using ThinkingHome.Core.Plugins;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

[assembly: MigrationAssembly("ThinkingHome.Plugins.Mqtt")]

namespace ThinkingHome.Plugins.Mqtt
{
	[Plugin]
	public class MqttPlugin : PluginBase
	{
		#region settings

		private const int DEFAULT_PORT = 1883;

		private string Host
		{
			get { return ConfigurationManager.AppSettings["Mqtt.Host"]; }
		}

		private int Port
		{
			get
			{
				int port;
				return int.TryParse(ConfigurationManager.AppSettings["Mqtt.Host"], out port)
					? port
					: DEFAULT_PORT;
			}
		}

		private string Login
		{
			get { return ConfigurationManager.AppSettings["Mqtt.Login"]; }
		}

		private string Password
		{
			get { return ConfigurationManager.AppSettings["Mqtt.Password"]; }
		}

		private string Path
		{
			get { return ConfigurationManager.AppSettings["Mqtt.Path"]; }
		}

		#endregion

		private MqttClient client;

		private const string SETTINGS_MESSAGE_FORMAT = "{0} is required but it is not specified - check the \"{1}\" parameter";

		public override void InitPlugin()
		{
			Debugger.Launch();

			bool isValidSettings = true;

			if (string.IsNullOrWhiteSpace(Host))
			{
				Logger.Warn(SETTINGS_MESSAGE_FORMAT, "mqtt host", "Mqtt.Host");
				isValidSettings = false;
			}

			if (string.IsNullOrWhiteSpace(Path))
			{
				Logger.Warn(SETTINGS_MESSAGE_FORMAT, "mqtt subscription path", "Mqtt.Path");
				isValidSettings = false;
			}

			if (isValidSettings)
			{
				Logger.Info("init mqtt client: {0}:{1}", Host, Port);
				client = new MqttClient(Host, Port, false, null);

				client.ConnectionClosed += client_ConnectionClosed;
				client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
			}
		}

		void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
		{
			Logger.Info("mqtt client {0}: message received\ntopic: {1}, message: {2}", ((MqttClient)sender).ClientId, e.Topic, Encoding.UTF8.GetString(e.Message));
		}

		void client_ConnectionClosed(object sender, EventArgs e)
		{
			Logger.Info("mqtt client {0}: connection closed", ((MqttClient)sender).ClientId);
		}

		public override void StartPlugin()
		{
			if (client != null)
			{
				var clientId = Guid.NewGuid().ToString();

				if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
				{
					Logger.Info("connect to mqtt broker using clientId: {0}", clientId);
					client.Connect(clientId);
				}
				else
				{
					Logger.Info("connect to mqtt broker using clientId: {0}, login: {1}", clientId, Login);
					client.Connect(clientId, Login, Password);
				}

				client.Subscribe(new[] { Path }, new[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
			}
		}

		public override void StopPlugin()
		{
			Logger.Info("disconnect mqtt client: id {0}", client.ClientId);
			client.Disconnect();
			base.StopPlugin();
		}
	}
}
