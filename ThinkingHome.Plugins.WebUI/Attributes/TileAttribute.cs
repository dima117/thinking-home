using System;
using System.ComponentModel.Composition;
using ThinkingHome.Plugins.WebUI.Model;

namespace ThinkingHome.Plugins.WebUI.Attributes
{
	public class TileAttribute : ExportAttribute
	{
		public TileAttribute()
			: base("FA4F97A0-41A0-4A72-BEF3-6DB579D909F4", typeof(Func<Guid, TileModel>))
		{}
	}
}
