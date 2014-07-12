using System;
using ThinkingHome.Plugins.WebUI.Tiles;

namespace ThinkingHome.Plugins.WebUI.Model
{
	public class TileModel
	{
		public TileModel(Guid tileIid, TileDefinition def)
		{
			id = tileIid;
			title = def.Title;
			wide = def.IsWide;
			url = def.Url;
		}

		// ReSharper disable InconsistentNaming
		public Guid id = Guid.NewGuid();
		public string title;
		public bool wide;
		public string url;
		public string content;
		public string className;
		// ReSharper restore InconsistentNaming

	}
}