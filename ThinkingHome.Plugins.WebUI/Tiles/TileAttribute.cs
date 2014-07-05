using System;
using System.ComponentModel.Composition;

namespace ThinkingHome.Plugins.WebUI.Tiles
{
	public interface ITileAttribute
	{
		string Key { get; }
	}

	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class TileAttribute : ExportAttribute, ITileAttribute
	{
		public string Key { get; private set; }

		public TileAttribute(string key) : base("FA4F97A0-41A0-4A72-BEF3-6DB579D909F4", typeof (TileDefinition))
		{
			Key = key;
		}
	}
}
