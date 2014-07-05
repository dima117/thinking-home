using System;
using System.ComponentModel.Composition;

namespace ThinkingHome.Plugins.WebUI.Tiles
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class TileAttribute : ExportAttribute
	{
		public TileAttribute() : base("FA4F97A0-41A0-4A72-BEF3-6DB579D909F4", typeof (TileDefinition))
		{
		}
	}
}
