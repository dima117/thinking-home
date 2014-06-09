using System;

namespace ThinkingHome.Plugins.WebUI.Model
{
	public class TileModel
	{
		// ReSharper disable InconsistentNaming
		public Guid id = Guid.NewGuid();
		public string title;
		public bool wide;
		public string[] content;
		// ReSharper restore InconsistentNaming
	}
}