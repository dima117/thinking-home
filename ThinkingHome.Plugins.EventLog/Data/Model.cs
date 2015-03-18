using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkingHome.Plugins.EventLog.Data
{
	public class EventLogItem
	{
		public virtual Guid Id { get; set; }

		public virtual DateTime Timestamp { get; set; }
	}
}
