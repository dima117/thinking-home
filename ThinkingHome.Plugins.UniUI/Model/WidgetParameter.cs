using System;

namespace ThinkingHome.Plugins.UniUI.Model
{
	public class WidgetParameter
	{
		public virtual Guid Id { get; set; }

		public virtual Widget Widget { get; set; }

		public virtual string Key { get; set; }

		public virtual Guid? ValueGuid { get; set; }

		public virtual int? ValueInt { get; set; }

		public virtual string ValueString { get; set; }
	}
}
