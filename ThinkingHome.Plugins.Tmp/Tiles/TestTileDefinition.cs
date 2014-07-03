using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkingHome.Plugins.WebUI.Tiles;

namespace ThinkingHome.Plugins.AlarmClock
{
	[Tile("1FE49988-B300-4931-85D5-531CACC3EC71", "Test", "webapp/alarm-clock/list")]
	public class TestTileDefinition : TileDefinition
	{
		public override void FillModel(WebUI.Model.TileModel model)
		{
			model.className = "btn-danger";
			model.content = "This is a test";
		}
	}
}
