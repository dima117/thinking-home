using System.Linq;
using NHibernate;
using NLog;
using ThinkingHome.Plugins.Microclimate.Model;
using ThinkingHome.Plugins.UniUI.Model;
using ThinkingHome.Plugins.UniUI.Widgets;

namespace ThinkingHome.Plugins.Microclimate
{
	// todo: поменять ключ параметра на Guid
	// todo: добавить alias для типа виджета в атрибут

	[Widget]
	public class MicroclimateWidgetDefinition : IWidgetDefinition
	{
		public object GetWidgetData(Widget widget, WidgetParameter[] parameters, ISession session, Logger logger)
		{
			var sensorId = parameters.First(p => p.Key == "sensorId").ValueGuid;
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
			throw new System.NotImplementedException();
		}
	}
}
