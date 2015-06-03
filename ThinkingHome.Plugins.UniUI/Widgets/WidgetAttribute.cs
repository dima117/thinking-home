using System;
using System.ComponentModel.Composition;

namespace ThinkingHome.Plugins.UniUI.Widgets
{
	public interface IWidgetAttribute
	{
		string TypeAlias { get; }
	}

	[MetadataAttribute, AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class WidgetAttribute : ExportAttribute, IWidgetAttribute
	{
		public WidgetAttribute(string typeAlias)
			: base("ABD9D425-5836-4DC5-88B6-222CD7A658CA", typeof(IWidgetDefinition))
		{
			TypeAlias = typeAlias;
		}

		public string TypeAlias { get; private set; }
	}
}
