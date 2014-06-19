using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Net;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Mapping.ByCode;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Core.Plugins.Utils;
using ThinkingHome.Plugins.Listener;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Weather.Data;

namespace ThinkingHome.Plugins.Weather
{
	[Plugin]
	public class WeatherPlugin : Plugin
	{
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

		private dynamic LoadForecast(string cityName)
		{
			string encodedCityName = WebUtility.UrlEncode(cityName);
			string url = string.Format(SERVICE_URL_FORMAT, encodedCityName);

			using (var client = new WebClient())
			{
				var json = client.DownloadString(url);
				return Extensions.FromJson(json);
			}
		}

		[HttpCommand("/api/weather/test")]
		public object GetAlarmList(HttpRequestParams request)
		{
			using (var session = Context.OpenSession())
			{
				var locations = session.Query<Location>().ToList();

				foreach (var location in locations)
				{
					var forecast = LoadForecast(location.Query);
					var list = forecast.list as IEnumerable;

					if (list != null)
					{
						foreach (dynamic item in list)
						{
							long seconds = item.dt;

							var dataItem = GetWeatherData(seconds, session, location);
							UpdateDataItem(dataItem, item);
							session.Flush();
						}
					}
				}

				session.Flush();
			}

			return null;
		}

		private static WeatherData GetWeatherData(long seconds, ISession session, Location location)
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

		private static void UpdateDataItem(WeatherData dataItem, dynamic item)
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

	}
}
