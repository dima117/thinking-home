using System;

namespace ThinkingHome.Plugins.UniUI.Widgets
{
	public class WidgetParameterMetaData
	{
		public string DisplayName { get; set; }

		public Guid Key { get; set; }

		public WidgetParameterType Type { get; set; }

		public WidgetSelectItem[] Items { get; set; }
	}
}
