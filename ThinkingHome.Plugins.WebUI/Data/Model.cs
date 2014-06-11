using System;

namespace ThinkingHome.Plugins.WebUI.Data
{
	public class Tile
	{
		public virtual Guid Id { get; set; }

		public virtual string HandlerKey { get; set; }

		public virtual int SortOrder { get; set; }
	}
}
