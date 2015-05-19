using System;

namespace ThinkingHome.Plugins.Mqtt.Model
{
	public class Subscription
	{
		public virtual Guid Id { get; set; }

		public virtual Broker Broker { get; set; }

		public virtual string Path { get; set; }
	}
}
