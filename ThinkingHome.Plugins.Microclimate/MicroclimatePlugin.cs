using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ECM7.Migrator.Framework;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Mapping.ByCode;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.Microclimate.Model;
using ThinkingHome.Plugins.NooLite;
using ThinkingHome.Plugins.Scripts;
using ThinkingHome.Plugins.WebUI.Attributes;

[assembly: MigrationAssembly("ThinkingHome.Plugins.Microclimate")]

namespace ThinkingHome.Plugins.Microclimate
{
	[Plugin]

	[AppSection("Microclimate sensors", SectionType.System, "/webapp/microclimate/settings.js", "ThinkingHome.Plugins.Microclimate.Resources.settings.js")]
	[JavaScriptResource("/webapp/microclimate/settings-view.js", "ThinkingHome.Plugins.Microclimate.Resources.settings-view.js")]
	[JavaScriptResource("/webapp/microclimate/settings-model.js", "ThinkingHome.Plugins.Microclimate.Resources.settings-model.js")]

	[JavaScriptResource("/webapp/microclimate/details.js", "ThinkingHome.Plugins.Microclimate.Resources.details.js")]
	[JavaScriptResource("/webapp/microclimate/details-view.js", "ThinkingHome.Plugins.Microclimate.Resources.details-view.js")]
	[JavaScriptResource("/webapp/microclimate/details-model.js", "ThinkingHome.Plugins.Microclimate.Resources.details-model.js")]

	[HttpResource("/webapp/microclimate/details-template.tpl", "ThinkingHome.Plugins.Microclimate.Resources.details-template.tpl")]
	[HttpResource("/webapp/microclimate/settings.tpl", "ThinkingHome.Plugins.Microclimate.Resources.settings.tpl")]
	[HttpResource("/webapp/microclimate/settings-row.tpl", "ThinkingHome.Plugins.Microclimate.Resources.settings-row.tpl")]
	[CssResource("/webapp/microclimate/microclimate.css", "ThinkingHome.Plugins.Microclimate.Resources.microclimate.css", AutoLoad = true)]
	
	public class MicroclimatePlugin : PluginBase
	{
		public const int PERIOD = 36;	// in hours

		public override void InitDbModel(ModelMapper mapper)
		{
			mapper.Class<TemperatureSensor>(cfg => cfg.Table("Microclimate_TemperatureSensor"));
			mapper.Class<TemperatureData>(cfg => cfg.Table("Microclimate_TemperatureData"));
		}

		[OnMicroclimateDataReceived]
		public void MicroclimateDataReceived(int channel, decimal temperature, int humidity)
		{
			var now = DateTime.Now;

			Logger.Debug("microclimate data received: c={0}, t={1}, h={2}", channel, temperature, humidity);

			using (ISession session = Context.OpenSession())
			{
				var sensors = session
					.Query<TemperatureSensor>()
					.Where(s => s.Channel == channel)
					.ToList();

				Logger.Debug("{0} sensors was found", sensors.Count);

				foreach (var sensor in sensors)
				{
					var intTemperature = Convert.ToInt32(temperature);

					sensor.CurrentTemperature = intTemperature;
					sensor.CurrentHumidity = humidity;
					sensor.Timestamp = now;

					var data = new TemperatureData
					{
						Id = Guid.NewGuid(),
						CurrentDate = now,
						Temperature = intTemperature,
						Humidity = humidity,
						Sensor = sensor
					};

					session.Save(sensor);
					session.Save(data);
				}

				session.Flush();
			}
		}

		#region api: read

		[ScriptCommand("microclimateReadData")]
		public MicroclimateData ScriptRead(int channel)
		{
			return Read(channel);
		}

		public MicroclimateData Read(int channel, ISession session = null)
		{
			if (session == null)
			{
				using (session = Context.OpenSession())
				{
					return Read(channel, session);
				}
			}

			var sensor = session
				.Query<TemperatureSensor>()
				.FirstOrDefault(m => m.Channel == channel);

			return sensor == null
				? null
				: CreateMicroclimateData(sensor);
		}

		private MicroclimateData CreateMicroclimateData(TemperatureSensor sensor)
		{
			return new MicroclimateData
			{
				channel = sensor.Channel,
				temperature = sensor.CurrentTemperature,
				humidity = sensor.ShowHumidity ? sensor.CurrentHumidity : (int?)null,
				timestamp = sensor.Timestamp
			};
		}

		#endregion

		#region api

		[HttpCommand("/api/microclimate/sensors/table")]
		public object GetSensorTable(HttpRequestParams request)
		{
			using (var session = Context.OpenSession())
			{
				var sensors = session.Query<TemperatureSensor>().ToList();

				var model = sensors
					.Select(s => new
					{
						id = s.Id,
						displayName = s.DisplayName,
						channel = s.Channel,
						showHumidity = s.ShowHumidity ? "Yes" : "No"
					})
					.ToList();

				return model;
			}
		}

		[HttpCommand("/api/microclimate/sensors/details")]
		public object GetSensorDetails(HttpRequestParams request)
		{
			var sensorId = request.GetRequiredGuid("id");
			var now = DateTime.Now;
			var from = now.AddHours(-PERIOD);

			using (var session = Context.OpenSession())
			{
				var sensor = session.Query<TemperatureSensor>().First(s => s.Id == sensorId);

				var data = session.Query<TemperatureData>()
					.Where(d => d.Sensor.Id == sensor.Id && d.CurrentDate > from)
					.OrderByDescending(d => d.CurrentDate)
					.ToList();

				return CreateSensorDetailsItemModel(sensor, data, now);
			}
		}

		[HttpCommand("/api/microclimate/sensors/add")]
		public object AddSensor(HttpRequestParams request)
		{
			var displayName = request.GetRequiredString("displayName");
			var channel = request.GetRequiredInt32("channel");
			var showHumidity = request.GetRequiredBool("showHumidity");

			Logger.Debug("add sensor: channel={0}; displayName={1}; showHumidity={2}", channel, displayName, showHumidity);

			using (var session = Context.OpenSession())
			{
				var sensor = new TemperatureSensor
				{
					Id = Guid.NewGuid(),
					Channel = channel,
					DisplayName = displayName,
					ShowHumidity = showHumidity,
					Timestamp = DateTime.Now
				};

				session.Save(sensor);
				session.Flush();

				return sensor.Id;
			}
		}

		[HttpCommand("/api/microclimate/sensors/delete")]
		public object DeleteSensor(HttpRequestParams request)
		{
			var id = request.GetRequiredGuid("id");
			Logger.Debug("delete sensor: id={0}", id);

			using (var session = Context.OpenSession())
			{
				var sensor = session.Load<TemperatureSensor>(id);

				session.Delete(sensor);
				session.Flush();
			}

			return null;
		}

		#endregion

		#region private

		private object CreateSensorDetailsItemModel(TemperatureSensor sensor, IEnumerable<TemperatureData> gr, DateTime now)
		{
			return new
			{
				id = sensor.Id,
				displayName = sensor.DisplayName,
				showHumidity = sensor.ShowHumidity,
				data = gr.Select(d => CreateDataModel(d, now)).ToArray()
			};
		}

		private object CreateDataModel(TemperatureData data, DateTime now)
		{
			return data == null
				? null
				: new
				{
					d = data.CurrentDate,
					t = data.Temperature,
					h = data.Humidity,
					dd = data.CurrentDate.ToShortTimeString(),
					ddd = data.CurrentDate < now.AddDays(-1) ? data.CurrentDate.ToString("M") : null,
					dt = FormatTemperature(data.Temperature),
					dh = data.Humidity + "%"
				};
		}

		private string FormatTemperature(int t)
		{
			return t > 0 ? "+" + t : t.ToString(CultureInfo.InvariantCulture);
		}

		#endregion
	}
}
