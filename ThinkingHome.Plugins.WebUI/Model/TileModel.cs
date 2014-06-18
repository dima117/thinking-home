using System;

namespace ThinkingHome.Plugins.WebUI.Model
{
	public class TileModel
	{
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