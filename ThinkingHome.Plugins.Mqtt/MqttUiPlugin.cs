using System.Linq;
using NHibernate.Linq;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.Mqtt.Model;
using ThinkingHome.Plugins.WebUI.Attributes;

namespace ThinkingHome.Plugins.Mqtt
{
	[Plugin]

	[AppSection("MQTT received data", SectionType.System, "/webapp/mqtt/received-data.js", "ThinkingHome.Plugins.Mqtt.Resources.received-data.js")]
	[JavaScriptResource("/webapp/mqtt/received-data-model.js", "ThinkingHome.Plugins.Mqtt.Resources.received-data-model.js")]
	[JavaScriptResource("/webapp/mqtt/received-data-view.js", "ThinkingHome.Plugins.Mqtt.Resources.received-data-view.js")]
	[HttpEmbeddedResource("/webapp/mqtt/received-data-list.tpl", "ThinkingHome.Plugins.Mqtt.Resources.received-data-list.tpl")]
	[HttpEmbeddedResource("/webapp/mqtt/received-data-list-item.tpl", "ThinkingHome.Plugins.Mqtt.Resources.received-data-list-item.tpl")]
	public class MqttUiPlugin : PluginBase
	{
		[HttpCommand("/api/mqtt/messages")]
		public object GetSensorTable(HttpRequestParams request)
		{
			using (var session = Context.OpenSession())
			{
				var info = new
					{
						host = MqttPlugin.Host,
						port = MqttPlugin.Port,
						path = MqttPlugin.Path
					};

				var messages = session
								.Query<ReceivedData>()
								.OrderBy(m => m.Path)
								.Select(m => new
								{
									id = m.Id,
									path = m.Path,
									message = m.Message,
									timestamp = m.Timestamp
								}).ToList();


				return new { info, messages };
			}
		}

		[HttpCommand("/api/mqtt/messages/delete")]
		public object DeleteMessage(HttpRequestParams request)
		{
			var id = request.GetRequiredGuid("id");
			
			using (var session = Context.OpenSession())
			{
				var message = session.Load<ReceivedData>(id);

				if (message != null)
				{
					Logger.Debug("delete mqtt message: path={0}", message.Path);
					
					session.Delete(message);
					session.Flush();
				}
			}

			return null;
		}

	}
}
