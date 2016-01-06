using System.Linq;
using NHibernate;
using NHibernate.Linq;
using NLog;
using ThinkingHome.Plugins.Microclimate.Lang;
using ThinkingHome.Plugins.Microclimate.Model;
using ThinkingHome.Plugins.UniUI.Model;
using ThinkingHome.Plugins.UniUI.Widgets;

namespace ThinkingHome.Plugins.Microclimate
{
	[Widget("microclimate-sensor")]
	public class MicroclimateWidgetDefinition : IWidgetDefinition
	{
		private const string PARAM_SENSOR_ID = "sensor-id";

		public string DisplayName
		{
			get { return MicroclimateLang.Microclimate_sensor; }
		}

		public object GetWidgetData(Widget widget, WidgetParameter[] parameters, ISession session, Logger logger)
		{
			var sensorId = parameters.First(p => p.Name == PARAM_SENSOR_ID).ValueGuid;
			var sensor = session.Get<TemperatureSensor>(sensorId);

			return new
			{
				id = sensorId, 
				timestamp = sensor.Timestamp,
				t = sensor.CurrentTemperature,
				h = sensor.CurrentHumidity,
				showHumidity = sensor.ShowHumidity
			};
		}

		public WidgetParameterMetaData[] GetWidgetMetaData(ISession session, Logger logger)
		{
			var sensors = session
				.Query<TemperatureSensor>()
				.Select(s => new WidgetSelectItem(s.Id, s.DisplayName))
				.ToArray();

			var sensorIdParameter = new WidgetParameterMetaData
			{
				Name = PARAM_SENSOR_ID,
				DisplayName = MicroclimateLang.Sensor,
				Type = WidgetParameterType.Guid,
				Items = sensors
			};

			return new[] { sensorIdParameter };
		}
	}
}
