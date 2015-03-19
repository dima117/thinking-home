using System;
using NHibernate;
using ThinkingHome.Plugins.EventLog.Data;

namespace ThinkingHome.Plugins.EventLog.Api
{
	public class Event
	{
		public readonly ISession session;
		public readonly LogItem item;

		public Event(ISession session)
		{
			this.session = session;

			item = new LogItem { Id = Guid.NewGuid(), Timestamp = DateTime.Now };
			this.session.Save(item);
		}

		public void Flush()
		{
			session.Flush();
		}
	}

	public class Event<TEventData> : Event where TEventData : class, ILogItemData
	{
		public readonly TEventData data;

		public Event(ISession session, TEventData data)
			: base(session)
		{
			if (data == null)
			{
				throw new ArgumentNullException();
			}

			this.data = data;
			this.data.LogItem = item;
			this.session.Save(data);
		}
	}
}
