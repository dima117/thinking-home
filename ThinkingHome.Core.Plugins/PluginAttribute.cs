using System;
using System.ComponentModel.Composition;

namespace ThinkingHome.Core.Plugins
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class PluginAttribute : ExportAttribute
	{
		public PluginAttribute()
			: base(typeof(Plugin))
		{
		}
	}
}
