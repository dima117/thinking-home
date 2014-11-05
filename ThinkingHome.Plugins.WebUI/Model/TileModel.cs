using System;

namespace ThinkingHome.Plugins.WebUI.Model
{
	public class TileModel
	{
		public TileModel(Guid tileIid)
		{
			id = tileIid;
		}

		// ReSharper disable InconsistentNaming
		public Guid id = Guid.NewGuid();
		public string title;
		public bool wide;
		public string url;
		public object[] parameters;
		public string content;
		public string className;
		// ReSharper restore InconsistentNaming
	}
}