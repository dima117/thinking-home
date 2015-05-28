using System;

namespace ThinkingHome.Plugins.UniUI.Model
{
	public class Dashboard
	{
		public virtual Guid Id { get; set; }

		public virtual string Title { get; set; }

		public virtual int SortOrder { get; set; }
	}
}
