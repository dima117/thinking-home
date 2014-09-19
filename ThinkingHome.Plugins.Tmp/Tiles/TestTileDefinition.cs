using ThinkingHome.Plugins.WebUI.Model;
using ThinkingHome.Plugins.WebUI.Tiles;

namespace ThinkingHome.Plugins.Tmp.Tiles
{
	[Tile]
	public class TestTileDefinition : TileDefinition
	{
		public override void FillModel(TileModel model, dynamic options)
		{
			model.title = "Test";
			model.url = "webapp/alarm-clock/list";
			model.className = "btn-danger";
			model.content = "This is a test";
		}
	}
}
