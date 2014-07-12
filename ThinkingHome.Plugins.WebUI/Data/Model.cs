using System;
using ThinkingHome.Core.Plugins.Utils;

namespace ThinkingHome.Plugins.WebUI.Data
{
	public class Tile
	{
		public virtual Guid Id { get; set; }

		public virtual string HandlerKey { get; set; }

		public virtual int SortOrder { get; set; }

		public virtual string SerializedParameters { get; set; }

		public virtual dynamic GetParameters()
		{
			var json = string.IsNullOrWhiteSpace(SerializedParameters) ? "{}" : SerializedParameters;
			return Extensions.FromJson(json);
		}

		public virtual void SetParameters(object obj)
		{
			SerializedParameters = obj.ToJson();
		}
	}
}
