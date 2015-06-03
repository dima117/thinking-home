using System;
using System.ComponentModel.Composition;

namespace ThinkingHome.Plugins.UniUI.Widgets
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class WidgetAttribute : ExportAttribute
	{
		public WidgetAttribute()
			: base("ABD9D425-5836-4DC5-88B6-222CD7A658CA", typeof(IWidgetDefinition))
		{
		}
	}
}
