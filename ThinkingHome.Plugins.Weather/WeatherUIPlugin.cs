using System;
using System.Linq;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.Weather.Api;
using ThinkingHome.Plugins.WebUI.Attributes;

namespace ThinkingHome.Plugins.Weather
{
	[AppSection("Weather", SectionType.Common, "/webapp/weather/forecast.js", "ThinkingHome.Plugins.Weather.Resources.weather-forecast.js")]
	[JavaScriptResource("/webapp/weather/forecast-model.js", "ThinkingHome.Plugins.Weather.Resources.weather-forecast-model.js")]
	[JavaScriptResource("/webapp/weather/forecast-view.js", "ThinkingHome.Plugins.Weather.Resources.weather-forecast-view.js")]
	[HttpResource("/webapp/weather/forecast.tpl", "ThinkingHome.Plugins.Weather.Resources.weather-forecast.tpl")]
	[HttpResource("/webapp/weather/forecast-item.tpl", "ThinkingHome.Plugins.Weather.Resources.weather-forecast-item.tpl")]

	// css
	[CssResource("/webapp/weather/css/weather-icons.min.css", "ThinkingHome.Plugins.Weather.Resources.css.weather-icons.min.css", AutoLoad = true)]
	[CssResource("/webapp/weather/css/weather-forecast.css", "ThinkingHome.Plugins.Weather.Resources.css.weather-forecast.css", AutoLoad = true)]

	// fonts
	[HttpResource("/webapp/weather/fonts/weathericons-regular-webfont.eot", "ThinkingHome.Plugins.Weather.Resources.fonts.weathericons-regular-webfont.eot", "application/vnd.ms-fontobject")]
	[HttpResource("/webapp/weather/fonts/weathericons-regular-webfont.svg", "ThinkingHome.Plugins.Weather.Resources.fonts.weathericons-regular-webfont.svg", "image/svg+xml")]
	[HttpResource("/webapp/weather/fonts/weathericons-regular-webfont.ttf", "ThinkingHome.Plugins.Weather.Resources.fonts.weathericons-regular-webfont.ttf", "application/x-font-truetype")]
	[HttpResource("/webapp/weather/fonts/weathericons-regular-webfont.woff", "ThinkingHome.Plugins.Weather.Resources.fonts.weathericons-regular-webfont.woff", "application/font-woff")]


	[Plugin]
	public class WeatherUIPlugin : Plugin
	{
		[HttpCommand("/api/weather/update")]
		public object UpdateWeather(HttpRequestParams request)
		{
			Context.GetPlugin<WeatherPlugin>().ReloadWeatherData();

			return null;
		}


		[HttpCommand("/api/weather/all")]
		public object GetWeather(HttpRequestParams request)
		{
			var now = DateTime.Now;

			WeatherLocatioinModel[] data2 = Context
				.GetPlugin<WeatherPlugin>()
				.GetWeatherData(now);

			return data2.Select(BuildLocationModel).ToArray();
		}

		#region private

		private static string FormatTemperature(int t)
		{
			string format = t > 0 ? "+{0}" : "{0}";
			return string.Format(format, t);
		}

		private static string FormatTemperatureRange(int t1, int t2)
		{
			return t1 == t2
				? FormatTemperature(t1)
				: string.Format("{0} .. {1}", FormatTemperature(t1), FormatTemperature(t2));
		}

		private static string GetIconClass(string code)
		{
			switch (code)
			{
				// DAY
				case "01d":	// sky is clear 
					return "wi-day-sunny";
				case "02d": // few clouds
					return "wi-day-cloudy";
				case "03d": // scattered clouds
					return "wi-cloudy";
				case "04d": // broken clouds
					return "wi-cloudy";
				case "09d": // shower rain
					return "wi-showers";
				case "10d": // rain
					return "wi-day-rain-mix";
				case "11d": // thunderstorm
					return "wi-storm-showers";
				case "13d": // snow
					return "wi-snow";
				case "50d": // mist
					return "wi-fog";

				// NIGHT
				case "01n":	// sky is clear 
					return "wi-stars";
				case "02n": // few clouds
					return "wi-night-cloudy";
				case "03n": // scattered clouds
					return "wi-cloudy";
				case "04n": // broken clouds
					return "wi-cloudy";
				case "09n": // shower rain
					return "wi-showers";
				case "10n": // rain
					return "wi-night-rain-mix";
				case "11n": // thunderstorm
					return "wi-storm-showers";
				case "13n": // snow
					return "wi-snow";
				case "50n": // mist
					return "wi-fog";
				default:
					return "wi-cloud-refresh";
			}
		}

		private object BuildModel(WeatherDataModel data)
		{
			return data == null
				? null
				: new
					{
						date = data.DateTime.ToString("M"),
						time = data.DateTime.ToShortTimeString(),
						t = FormatTemperature(data.Temperature),
						p = data.Pressure,
						h = data.Humidity,
						icon = GetIconClass(data.Code),
						description = data.Description
					};
		}

		private object BuildModel(DailyWeatherDataModel data)
		{
			return data == null
				? null
				: new
				{
					date = data.DateTime.ToString("M"),
					time = data.DateTime.ToShortTimeString(),
					t = FormatTemperatureRange(data.MinTemperature, data.MaxTemperature),
					p = string.Format("{0} .. {1}", data.MinPressure, data.MaxPressure),
					h = string.Format("{0} .. {1}", data.MinHumidity, data.MaxHumidity),
					icon = GetIconClass(data.Code),
					description = data.Description
				};
		}

		private object BuildLocationModel(WeatherLocatioinModel data)
		{
			return new
			{
				name = data.LocationName,
				now = BuildModel(data.Now),
				day = data.Today.Select(BuildModel).ToArray(),
				forecast = data.Forecast.Select(BuildModel).ToArray()
			};
		}

		#endregion
	}
}
