using ThinkingHome.Plugins.WebUI.Tiles;

namespace ThinkingHome.Plugins.Tmp.Tiles
{
	[Tile("1FE49988-B300-4931-85D5-531CACC3EC71")]
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

		public override void FillModel(WebUI.Model.TileModel model)
		{
			model.className = "btn-danger";
			model.content = "This is a test";
		}
	}
}
