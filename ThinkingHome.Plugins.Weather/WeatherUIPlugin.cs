using System;
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
	// forecast
	[AppSection("Weather", SectionType.Common, "/webapp/weather/forecast.js", "ThinkingHome.Plugins.Weather.Resources.js.forecast.forecast.js")]
	[JavaScriptResource("/webapp/weather/forecast-model.js", "ThinkingHome.Plugins.Weather.Resources.js.forecast.forecast-model.js")]
	[JavaScriptResource("/webapp/weather/forecast-view.js", "ThinkingHome.Plugins.Weather.Resources.js.forecast.forecast-view.js")]

	[HttpResource("/webapp/weather/forecast.tpl", "ThinkingHome.Plugins.Weather.Resources.js.forecast.forecast.tpl")]
	[HttpResource("/webapp/weather/forecast-item.tpl", "ThinkingHome.Plugins.Weather.Resources.js.forecast.forecast-item.tpl")]
	[HttpResource("/webapp/weather/forecast-item-value.tpl", "ThinkingHome.Plugins.Weather.Resources.js.forecast.forecast-item-value.tpl")]
	[HttpResource("/webapp/weather/forecast-item-value-now.tpl", "ThinkingHome.Plugins.Weather.Resources.js.forecast.forecast-item-value-now.tpl")]

	// settings
	[AppSection("Weather locations", SectionType.System, "/webapp/weather/locations.js", "ThinkingHome.Plugins.Weather.Resources.js.settings.locations.js")]
	[JavaScriptResource("/webapp/weather/locations-model.js", "ThinkingHome.Plugins.Weather.Resources.js.settings.locations-model.js")]
	[JavaScriptResource("/webapp/weather/locations-view.js", "ThinkingHome.Plugins.Weather.Resources.js.settings.locations-view.js")]

	[HttpResource("/webapp/weather/locations-layout.tpl", "ThinkingHome.Plugins.Weather.Resources.js.settings.locations-layout.tpl")]
	[HttpResource("/webapp/weather/locations-list.tpl", "ThinkingHome.Plugins.Weather.Resources.js.settings.locations-list.tpl")]
	[HttpResource("/webapp/weather/locations-list-item.tpl", "ThinkingHome.Plugins.Weather.Resources.js.settings.locations-list-item.tpl")]
	[HttpResource("/webapp/weather/locations-form.tpl", "ThinkingHome.Plugins.Weather.Resources.js.settings.locations-form.tpl")]

	// css
	[CssResource("/webapp/weather/css/weather-icons.min.css", "ThinkingHome.Plugins.Weather.Resources.css.weather-icons.min.css", AutoLoad = true)]
	[CssResource("/webapp/weather/css/weather-forecast.css", "ThinkingHome.Plugins.Weather.Resources.css.weather-forecast.css", AutoLoad = true)]

	// fonts
	[HttpResource("/webapp/weather/fonts/weathericons-regular-webfont.eot", "ThinkingHome.Plugins.Weather.Resources.fonts.weathericons-regular-webfont.eot", "application/vnd.ms-fontobject")]
	[HttpResource("/webapp/weather/fonts/weathericons-regular-webfont.svg", "ThinkingHome.Plugins.Weather.Resources.fonts.weathericons-regular-webfont.svg", "image/svg+xml")]
	[HttpResource("/webapp/weather/fonts/weathericons-regular-webfont.ttf", "ThinkingHome.Plugins.Weather.Resources.fonts.weathericons-regular-webfont.ttf", "application/x-font-truetype")]
	[HttpResource("/webapp/weather/fonts/weathericons-regular-webfont.woff", "ThinkingHome.Plugins.Weather.Resources.fonts.weathericons-regular-webfont.woff", "application/font-woff")]


	[Plugin]
	public class WeatherUIPlugin : PluginBase
	{
		[HttpCommand("/api/weather/locations/add")]
		public object AddLocation(HttpRequestParams request)
		{
			var displayName = request.GetRequiredString("displayName");
			var query = request.GetRequiredString("query");

			using (var session = Context.OpenSession())
			{
				var location = new Location
							   {
								   Id = Guid.NewGuid(),
								   DisplayName = displayName,
								   Query = query
							   };

				session.Save(location);
				session.Flush();
			}
			
			return null;
		}

		[HttpCommand("/api/weather/locations/delete")]
		public object DeleteLocation(HttpRequestParams request)
		{
			var locationId = request.GetRequiredGuid("locationId");
			using (var session = Context.OpenSession())
			{
				var location = session.Get<Location>(locationId);

				if (location != null)
				{
					session.Delete(location);
					session.Flush();	
				}
			}

			return null;
		}

		[HttpCommand("/api/weather/update")]
		public object UpdateAllWeather(HttpRequestParams request)
		{
			Context.GetPlugin<WeatherPlugin>().ReloadWeatherData();

			return null;
		}

		[HttpCommand("/api/weather/locations/update")]
		public object UpdateLocationWeather(HttpRequestParams request)
		{
			var locationId = request.GetRequiredGuid("locationId");
			Context.GetPlugin<WeatherPlugin>().ReloadWeatherData(locationId);

			return null;
		}

		[HttpCommand("/api/weather/all")]
		public object GetWeather(HttpRequestParams request)
		{
			var now = DateTime.Now;

			WeatherLocatioinModel[] weatherData = Context
				.GetPlugin<WeatherPlugin>()
				.GetWeatherData(now);

			return weatherData.Select(BuildLocationModel).ToArray();
		}

		[HttpCommand("/api/weather/locations/list")]
		public object GetLocations(HttpRequestParams request)
		{
			using (var session = Context.OpenSession())
			{
				var locations = session.Query<Location>()
					.OrderBy(l => l.DisplayName)
					.ToList();

				var model = locations
					.Select(l => new
						{
							id = l.Id,
							displayName = l.DisplayName,
							query = l.Query
						})
					.ToList();

				return model;
			}
		}

		#region private

		private object BuildModel(WeatherDataModel data)
		{
			return data == null
				? null
				: new
					{
						when = data.DateTime.ToShortTimeString(),
						t = WeatherUtils.FormatTemperature(data.Temperature),
						p = data.Pressure,
						h = data.Humidity,
						icon = WeatherUtils.GetIconClass(data.Code),
						description = data.Description
					};
		}

		private object BuildModel(DailyWeatherDataModel data)
		{
			return data == null
				? null
				: new
				{
					when = data.DateTime.ToString("M"),
					t = WeatherUtils.FormatTemperatureRange(data.MinTemperature, data.MaxTemperature),
					p = string.Format("{0} .. {1}", data.MinPressure, data.MaxPressure),
					h = string.Format("{0} .. {1}", data.MinHumidity, data.MaxHumidity),
					icon = WeatherUtils.GetIconClass(data.Code),
					description = data.Description
				};
		}

		private object BuildLocationModel(WeatherLocatioinModel data)
		{
			return new
			{
				id = data.LocationId,
				name = data.LocationName,
				now = BuildModel(data.Now),
				day = data.Today.Select(BuildModel).ToArray(),
				forecast = data.Forecast.Select(BuildModel).ToArray()
			};
		}

		#endregion
	}
}
