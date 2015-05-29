using System;

namespace ThinkingHome.Plugins.UniUI.Model
{
	public class Widget
	{
		public virtual Guid Id { get; set; }

		public virtual Dashboard Dashboard { get; set; }

		public virtual string TypeAlias { get; set; }

		public virtual int SortOrder { get; set; }
	}
}
