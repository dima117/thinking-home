using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using NLog;
using ThinkingHome.Plugins.Microclimate.Model;
using ThinkingHome.Plugins.UniUI.Model;
using ThinkingHome.Plugins.UniUI.Widgets;

namespace ThinkingHome.Plugins.Microclimate
{
	[Widget("microclimate-sensor")]
	public class MicroclimateWidgetDefinition : IWidgetDefinition
	{
		private static readonly Guid sensorIdKey = new Guid("1099AD53-9CA1-44F7-A871-BCD773C50D7F");

		public object GetWidgetData(Widget widget, WidgetParameter[] parameters, ISession session, Logger logger)
		{
			var sensorId = parameters.First(p => p.Key == sensorIdKey).ValueGuid;
			var sensor = session.Get<TemperatureSensor>(sensorId);

			return new
			{
				t = sensor.CurrentTemperature,
				h = sensor.ShowHumidity ? sensor.CurrentHumidity : (int?)null,
				timestamp = sensor.Timestamp
			};
		}

		public WidgetParameterMetaData[] GetWidgetMetaData(ISession session, Logger logger)
		{
			var sensors = session
				.Query<TemperatureSensor>()
				.Select(s => new WidgetSelectItem(s.Id.ToString(), s.DisplayName))
				.ToArray();

			var sensorIdParameter = new WidgetParameterMetaData
			{
				DisplayName = "Sensor",
				Key = sensorIdKey,
				Type = WidgetParameterType.Guid,
				Items = sensors
			};

			return new[] { sensorIdParameter };
		}
	}
}
