using System.Diagnostics;
using System.Net;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Core.Plugins.Utils;
using ThinkingHome.Plugins.Listener;
using ThinkingHome.Plugins.Listener.Api;

namespace ThinkingHome.Plugins.Weather
{
	[Plugin]
	public class WeatherPlugin : Plugin
	{
		private const string SERVICE_URL_FORMAT = "http://api.openweathermap.org/data/2.5/forecast?q={0}&units=metric";

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
			Debugger.Launch();
			var data = LoadForecast("Moscow,ru");

			return data.list[0].main;
		}

	}
}
