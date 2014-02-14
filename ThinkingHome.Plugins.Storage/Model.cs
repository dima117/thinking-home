using System;

namespace ThinkingHome.Plugins.Storage
{
	public class Item
	{
		public virtual string Id { get; set; }

		public virtual int? IntegerValue { get; set; }

		public virtual DateTime? DateTimeValue { get; set; }

		public virtual string StringValue { get; set; }

		public virtual string ObjectValue { get; set; }
	}
}
