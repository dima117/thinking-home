using System;

namespace ThinkingHome.Plugins.Mqtt.Model
{
	public class ReceivedData
	{
		public virtual Guid Id { get; set; }

		public virtual string Path { get; set; }

		public virtual DateTime Timestamp { get; set; }

		public virtual string Message { get; set; }
	}
}
