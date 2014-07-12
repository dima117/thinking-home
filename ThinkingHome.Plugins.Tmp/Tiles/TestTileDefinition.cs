using ThinkingHome.Plugins.WebUI.Model;
using ThinkingHome.Plugins.WebUI.Tiles;

namespace ThinkingHome.Plugins.Tmp.Tiles
{
	[Tile]
	public class TestTileDefinition : TileDefinition
	{
		public override string Title
		{
			get { return "Test"; }
		}

		public override string Url
		{
			get { return "webapp/alarm-clock/list"; }
		}

		public override void FillModel(TileModel model, dynamic options)
		{
			model.className = "btn-danger";
			model.content = "This is a test";
		}
	}
}
