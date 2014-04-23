using System;

namespace ThinkingHome.Plugins.WebUI.Data
{
	public class NavigationItem
	{
		public virtual Guid Id { get; set; }

		public virtual string Name { get; set; }

		public virtual string ModulePath { get; set; }

		public virtual int SortOrder { get; set; }
	}
}
