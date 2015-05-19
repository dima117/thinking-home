using System;

namespace ThinkingHome.Plugins.Mqtt.Model
{
	public class Broker
	{
		public virtual Guid Id { get; set; }

		public virtual string Alias { get; set; }

		public virtual string Host { get; set; }
		public virtual int? Port { get; set; }

		public virtual bool NeedsAuthorization { get; set; }
		public virtual string Login { get; set; }
		public virtual string Password { get; set; }
	}
}
