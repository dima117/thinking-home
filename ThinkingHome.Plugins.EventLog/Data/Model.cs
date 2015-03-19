using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
	}
}
