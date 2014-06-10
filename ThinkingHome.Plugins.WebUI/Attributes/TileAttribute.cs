using System;
using System.ComponentModel.Composition;
using ThinkingHome.Plugins.WebUI.Model;

namespace ThinkingHome.Plugins.WebUI.Attributes
{
	public interface ITileAttribute
	{
		string Key { get; }
		string Title { get; }
		string Url { get; }
		bool IsWide { get; }
	}

	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class TileAttribute : ExportAttribute, ITileAttribute
	{
		public string Key { get; private set; }
		public string Title { get; private set; }
		public string Url { get; private set; }

		public bool IsWide { get; set; }

		public TileAttribute(string key, string title, string url)
			: base("FA4F97A0-41A0-4A72-BEF3-6DB579D909F4", typeof (Action<TileModel>))
		{
			Key = key;
			Title = title;
			Url = url;
		}
	}
}
