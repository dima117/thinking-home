using ThinkingHome.Plugins.WebUI.Model;
using ThinkingHome.Plugins.WebUI.Tiles;

namespace ThinkingHome.Plugins.Tmp.Tiles
{
	[Tile]
	public class TestNewsTileDefinition : TileDefinition
	{
		public override void FillModel(TileModel model, dynamic options)
		{
			model.title = "News";
			model.url = "webapp/alarm-clock/list";
			model.wide = true;
			model.content = "NASA призвало Россию продлить сотрудничество по МКС\nНа спутнике Плутона мог существовать подземный океан";
		}
	}
}
