using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using ThinkingHome.Plugins.Microclimate.Model;
using ThinkingHome.Plugins.WebUI.Model;
using ThinkingHome.Plugins.WebUI.Tiles;

namespace ThinkingHome.Plugins.Microclimate
{
	[Tile]
	public class MicroclimateTileDefinition : TileDefinition
	{
		public override void FillModel(TileModel model, dynamic options)
		{
			try
			{
				var now = DateTime.Now;
				Tuple<TemperatureSensor, TemperatureData> tuple = GetSensorData(options, now);

				var sensor = tuple.Item1;
				var data = tuple.Item2;

				model.title = sensor.DisplayName;
				model.url = "webapp/microclimate/details";
				model.parameters = new object[] { options.id };

				if (data == null)
				{
					model.content = "The sensor has no data";
				}
				else
				{
					model.content = string.Format("t: {0}°C", data.Temperature);

					if (sensor.ShowHumidity)
					{
						model.content += string.Format("\r\nh: {0}%", data.Humidity);
					}

					model.content += "\r\non " + data.CurrentDate.ToShortTimeString();

					if (data.CurrentDate < now.AddDays(-1))
					{
						model.content += "\r\n" + data.CurrentDate.ToString("M");
					}
				}
			}
			catch (Exception ex)
			{
				model.content = ex.Message;
			}
		}

		private Tuple<TemperatureSensor, TemperatureData> GetSensorData(dynamic options, DateTime now)
		{
			string strId = options.id;

			if (string.IsNullOrWhiteSpace(strId))
			{
				throw new Exception("Missing id parameter");
			}

			Guid sensorId;

			if (!Guid.TryParse(strId, out sensorId))
			{
				throw new Exception("Id parameter must contain GUID value");
			}

			using (ISession session = Context.OpenSession())
			{
				var sensor = session.Get<TemperatureSensor>(sensorId);

				if (sensor == null)
				{
					var msg = string.Format("Sensor not found (id: {0})", sensorId);
					throw new Exception(msg);
				}

				var from = now.AddHours(-MicroclimatePlugin.PERIOD);
				var data = session
					.Query<TemperatureData>()
					.OrderByDescending(d => d.CurrentDate)
					.FirstOrDefault(d => d.CurrentDate > from && d.Sensor.Id == sensorId);

				return new Tuple<TemperatureSensor, TemperatureData>(sensor, data);
			}
		}
	}
}
