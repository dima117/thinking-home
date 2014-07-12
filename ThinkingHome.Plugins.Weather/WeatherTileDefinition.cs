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
			model.content = "10:00 — 5°C\n16:00 —  6°C\n22:00 —  4°C\n04:00 —  3°C";
		}
	}
}
