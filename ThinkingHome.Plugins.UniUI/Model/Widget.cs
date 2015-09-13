using System;

namespace ThinkingHome.Plugins.UniUI.Model
{
	public class Widget
	{
		public virtual Guid Id { get; set; }

		public virtual Panel Panel { get; set; }

		public virtual string TypeAlias { get; set; }
		
		public virtual string DisplayName { get; set; }

		public virtual int SortOrder { get; set; }
	}
}
