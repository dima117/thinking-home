using ThinkingHome.Plugins.WebUI.Model;
using ThinkingHome.Plugins.WebUI.Tiles;

namespace ThinkingHome.Plugins.WebUI
{
	[Tile]
	public class AppSectionShortcutTileDefinition : TileDefinition
	{
		public override string Title
		{
			get { return "section shortcut"; }
		}

		public override string Url
		{
			get { return null; }
		}

		public override void FillModel(TileModel model, dynamic options)
		{
			model.url = options.url;
			model.title = options.title;
		}
	}
}
