using ThinkingHome.Plugins.WebUI.Tiles;

namespace ThinkingHome.Plugins.Tmp.Tiles
{
	[Tile]
	public class TestNewsTileDefinition : TileDefinition
	{
		public override string Title
		{
			get { return "News"; }
		}

		public override string Url
		{
			get { return "webapp/alarm-clock/list"; }
		}

		public override bool IsWide
		{
			get { return true; }
		}

		public override void FillModel(WebUI.Model.TileModel model)
		{
			model.content = "NASA призвало Россию продлить сотрудничество по МКС\nНа спутнике Плутона мог существовать подземный океан";
		}
	}
}
