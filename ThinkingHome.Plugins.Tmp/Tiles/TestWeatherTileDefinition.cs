using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkingHome.Plugins.WebUI.Tiles;

namespace ThinkingHome.Plugins.AlarmClock
{
	[Tile("E6467D12-283A-4B07-B807-2FA2A7555ED4", "Weather", "webapp/alarm-clock/list")]
	public class TestWeatherTileDefinition : TileDefinition
	{
		public override void FillModel(WebUI.Model.TileModel model)
		{
			model.content = "10:00 — 5°C\n16:00 —  6°C\n22:00 —  4°C\n04:00 —  3°C";
		}
	}
}
