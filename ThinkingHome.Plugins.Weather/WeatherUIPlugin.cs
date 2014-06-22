using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Listener.Handlers;
using ThinkingHome.Plugins.WebUI.Attributes;

namespace ThinkingHome.Plugins.Weather
{
	[AppSection("Weather", SectionType.Common, "/webapp/weather/forecast.js", "ThinkingHome.Plugins.Weather.Resources.weather-forecast.js")]
	[JavaScriptResource("/webapp/weather/forecast-model.js", "ThinkingHome.Plugins.Weather.Resources.weather-forecast-model.js")]
	[JavaScriptResource("/webapp/weather/forecast-view.js", "ThinkingHome.Plugins.Weather.Resources.weather-forecast-view.js")]
	[HttpResource("/webapp/weather/forecast.tpl", "ThinkingHome.Plugins.Weather.Resources.weather-forecast.tpl")]

	[Plugin]
	public class WeatherUIPlugin : Plugin
	{
		
	}
}
