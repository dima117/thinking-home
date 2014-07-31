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
		public override string Title
		{
			get { return "Weather"; }
		}

		public override string Url
		{
			get { return "webapp/weather/forecast"; }
		}

		public override void FillModel(TileModel model, dynamic options)
		{
			string strCityId = options.cityId;

			if (string.IsNullOrWhiteSpace(strCityId))
			{
				throw new Exception("Missing cityId parameter");
			}

			Guid cityId;

			if (!Guid.TryParse(strCityId, out cityId))
			{
				throw new Exception("CityId parameter must contain GUID value");
			}

			var data = Context.GetPlugin<WeatherPlugin>().GetWeatherData(DateTime.Now);

			WeatherLocatioinModel location = data.FirstOrDefault(l => l.LocationId == cityId);

			if (location == null)
			{
				throw new Exception(string.Format("Location with id = {0} is not found", cityId));
			}

			model.title = location.LocationName;

			// текущая погода
			if (location.Now != null)
			{
				string formattedNow = WeatherUtils.FormatTemperature(location.Now.Temperature);
				model.content = string.Format("now: {0}°C", formattedNow);
				model.className = "btn-primary th-tile-icon th-tile-icon-wa " + WeatherUtils.GetIconClass(location.Now.Code);
			}
			else
			{
				throw new Exception("Current weather is undefined");
			}
			
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
