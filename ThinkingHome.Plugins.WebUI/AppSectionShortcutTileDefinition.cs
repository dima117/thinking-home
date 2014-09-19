using ThinkingHome.Plugins.WebUI.Model;
using ThinkingHome.Plugins.WebUI.Tiles;

namespace ThinkingHome.Plugins.WebUI
{
	[Tile]
	public class AppSectionShortcutTileDefinition : TileDefinition
	{
		public override void FillModel(TileModel model, dynamic options)
		{
			model.url = options.url;
			model.title = options.title;
			model.className = "btn-primary th-tile-icon th-tile-icon-fa fa-arrow-circle-right";
		}
	}
}
