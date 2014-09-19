using System;
using System.Linq;
using ThinkingHome.Plugins.Weather.Api;
using ThinkingHome.Plugins.WebUI.Model;
using ThinkingHome.Plugins.WebUI.Tiles;

namespace ThinkingHome.Plugins.Weather
{
	[Tile]
	public class WeatherTileDefinition : TileDefinition
	{
		public override void FillModel(TileModel model, dynamic options)
		{
			model.title = "Weather";
			model.url = "webapp/weather/forecast";

			string strCityId = options.cityId;

			if (string.IsNullOrWhiteSpace(strCityId))
			{
				model.content = "Missing cityId parameter";
				return;
			}

			Guid cityId;
			if (!Guid.TryParse(strCityId, out cityId))
			{
				model.content = "CityId parameter must contain GUID value";
				return;
			}

			var data = Context.GetPlugin<WeatherPlugin>().GetWeatherData(DateTime.Now);

			WeatherLocatioinModel location = data.FirstOrDefault(l => l.LocationId == cityId);

			if (location == null)
			{
				model.content = string.Format("Location with id = {0} is not found", cityId);
				return;
			}

			model.title = location.LocationName;

			// текущая погода
			if (location.Now == null)
			{
				model.content = "Current weather is undefined";
				return;
			}

			string formattedNow = WeatherUtils.FormatTemperature(location.Now.Temperature);
			model.content = string.Format("now: {0}°C", formattedNow);
			model.className = "btn-primary th-tile-icon th-tile-icon-wa " + WeatherUtils.GetIconClass(location.Now.Code);
			
			// погода на завтра
			var tomorrow = location.Forecast.FirstOrDefault();

			if (tomorrow != null)
			{
				string formattedTomorrow = WeatherUtils.FormatTemperatureRange(tomorrow.MinTemperature, tomorrow.MaxTemperature);
				model.content += string.Format("\nnext: {0}°C", formattedTomorrow);
			}
		}
	}
}
