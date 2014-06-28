using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.Weather.Api;
using ThinkingHome.Plugins.Weather.Data;
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
		[HttpCommand("/api/weather/all")]
		public object GetWeather(HttpRequestParams request)
		{
			using (var session = Context.OpenSession())
			{
				var now = DateTime.Now;
				var date = DateTime.Now.AddMinutes(-90);

				var locations = session.Query<Location>().ToArray();
				var data = session.Query<WeatherData>().Where(d => d.Date > date).ToArray();

				var list = new List<WeatherLocatioinModel>();

				foreach (var location in locations)
				{
					var locationModel = ModelBuilder.BuildLocatioinModel(now, location, data);
					list.Add(locationModel);
				}

				return list;
			}
		}

	}
}
