using System;

namespace ThinkingHome.Plugins.EventLog.Data
{
	public interface ILogItemData
	{
		LogItem LogItem { get; set; }
	}

	public class LogItem
	{
		public virtual Guid Id { get; set; }

		public virtual DateTime Timestamp { get; set; }

		public virtual string Data { get; set; }
	}
}
