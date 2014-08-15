using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Mapping.ByCode;
using NLog;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Core.Plugins.Utils;
using ThinkingHome.Plugins.Timer;
using ThinkingHome.Plugins.Weather.Api;
using ThinkingHome.Plugins.Weather.Data;

namespace ThinkingHome.Plugins.Weather
{
	[Plugin]
	public class WeatherPlugin : PluginBase
	{
		private const int UPDATE_PERIOD = 15;

		private readonly object autoUpdateLockObject = new object();
		private readonly object lockObject = new object();
		private DateTime lastUpdate = DateTime.Now;	// инициализируем значением now, чтобы на начало обновляться при старте приложения

		private const string SERVICE_URL_FORMAT = "http://api.openweathermap.org/data/2.5/forecast?q={0}&units=metric&APPID=9948774b7ea6673661f1bd773a48d23c";
		private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		public static DateTime DateTimeFromUnixTimestampSeconds(long seconds)
		{
			return UnixEpoch.AddSeconds(seconds);
		}

		public override void InitDbModel(ModelMapper mapper)
		{
			mapper.Class<Location>(cfg => cfg.Table("Weather_Location"));
			mapper.Class<WeatherData>(cfg => cfg.Table("Weather_Data"));
		}

		#region public

		public void ReloadWeatherData()
		{
			lock (lockObject)
			{
				using (var session = Context.OpenSession())
				{
					var locations = session.Query<Location>().ToList();

					foreach (var location in locations)
					{
						UpdateOneLocation(location, session);
					}
				}
			}
		}

		public void ReloadWeatherData(Guid locationId)
		{
			lock (lockObject)
			{
				using (var session = Context.OpenSession())
				{
					var location = session.Get<Location>(locationId);

					if (location != null)
					{
						UpdateOneLocation(location, session);
					}
				}
			}
		}

		#endregion

		#region events

		[OnTimerElapsed]
		public void OnTimerElapsed(DateTime now)
		{
			if (lastUpdate.AddMinutes(UPDATE_PERIOD) < now)
			{
				lock (autoUpdateLockObject)
				{
					if (lastUpdate.AddMinutes(UPDATE_PERIOD) < now)
					{
						Logger.Info("update all locations (last update {0})", lastUpdate);

						ReloadWeatherData();
						lastUpdate = now;

						Logger.Info("update completed");
					}
				}
			}
		}

		#endregion

		#region loading

		private void UpdateOneLocation(Location location, ISession session)
		{
			try
			{
				var forecast = LoadForecast(location.Query, Logger);
				UpdateWeatherData(session, location, forecast, Logger);
			}
			catch (Exception ex)
			{
				string msg = string.Format("loading error (location {0})", location);
				Logger.ErrorException(msg, ex);
			}
		}

		private static dynamic LoadForecast(string query, Logger logger)
		{
			string encodedCityName = WebUtility.UrlEncode(query);
			string url = string.Format(SERVICE_URL_FORMAT, encodedCityName);

			logger.Info("send request: {0}", url);

			using (var client = new WebClient())
			{
				var json = client.DownloadString(url);
				logger.Info("complete");

				return Extensions.FromJson(json);
			}
		}

		private static void UpdateWeatherData(ISession session, Location location, dynamic forecast, Logger logger)
		{
			var list = forecast.list as IEnumerable;

			int count = 0;

			if (list != null)
			{
				foreach (dynamic item in list)
				{
					long seconds = item.dt;

					var dataItem = GetWeatherDataItem(seconds, session, location);
					UpdateWeatherDataItem(dataItem, item);
					session.Flush();

					count++;
				}
			}

			logger.Info("updated {0} items", count);
		}

		private static WeatherData GetWeatherDataItem(long seconds, ISession session, Location location)
		{
			var date = DateTimeFromUnixTimestampSeconds(seconds);

			var dataItem = session
				.Query<WeatherData>()
				.FirstOrDefault(obj => obj.Date == date && obj.Location.Id == location.Id);

			if (dataItem == null)
			{
				dataItem = new WeatherData { Id = Guid.NewGuid(), Date = date, Location = location };
				session.Save(dataItem);
			}

			return dataItem;
		}

		private static void UpdateWeatherDataItem(WeatherData dataItem, dynamic item)
		{
			dataItem.Temperature = item.main.temp;
			dataItem.Cloudiness = item.clouds.all;
			dataItem.Humidity = item.main.humidity;
			dataItem.Pressure = item.main.pressure;
			dataItem.WindDirection = item.wind.deg;
			dataItem.WindSpeed = item.wind.speed;

			if (item.weather != null && item.weather.First != null)
			{
				dynamic w = item.weather.First;

				dataItem.WeatherDescription = w.description;
				dataItem.WeatherCode = w.icon;
			}
			else
			{
				dataItem.WeatherCode = dataItem.WeatherDescription = null;
			}
		}

		#endregion

		public WeatherLocatioinModel[] GetWeatherData(DateTime now)
		{
			var list = new List<WeatherLocatioinModel>();

			using (var session = Context.OpenSession())
			{
				var locations = session.Query<Location>().ToArray();

				var data = session.Query<WeatherData>()
							.Where(d => d.Date >= now.Date)
							.ToArray();

				foreach (var location in locations)
				{
					var locationData = data.Where(d => d.Location.Id == location.Id).ToArray();

					var model = ModelBuilder.LoadLocationWeatherData(now, location, locationData);
					list.Add(model);
				}
			}

			return list.ToArray();
		}
	}
}
