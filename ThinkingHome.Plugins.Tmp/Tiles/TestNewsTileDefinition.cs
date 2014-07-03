using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkingHome.Plugins.WebUI.Tiles;

namespace ThinkingHome.Plugins.AlarmClock
{
	[Tile("1DF06D69-4603-49C8-AA96-FF77BC0EB6D2", "News", "webapp/alarm-clock/list", IsWide = true)]
	public class TestNewsTileDefinition : TileDefinition
	{
		public override void FillModel(WebUI.Model.TileModel model)
		{
			model.content = "NASA призвало Россию продлить сотрудничество по МКС\nНа спутнике Плутона мог существовать подземный океан";
		}
	}
}
