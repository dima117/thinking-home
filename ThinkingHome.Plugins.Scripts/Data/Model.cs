using System;

namespace ThinkingHome.Plugins.Scripts.Data
{
	public class UserScript
	{
		public virtual Guid Id { get; set; }

		public virtual string Name { get; set; }

		public virtual string Body { get; set; }
	}

	public class ScriptEventHandler
	{
		public virtual Guid Id { get; set; }

		public virtual string PluginAlias { get; set; }

		public virtual string EventAlias { get; set; }

		public virtual UserScript UserScript { get; set; }
	}
}
