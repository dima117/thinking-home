using System;
using System.Reflection;

namespace ThinkingHome.Plugins.WebUI.Attributes
{
	public class AppSectionAttribute : JavaScriptResourceAttribute
	{
		public string Title { get; set; }

		public int SortOrder { get; set; }

		public SectionType Type { get; set; }

		public Type LangResourceType { get; set; }
		
		public string LangResourceKey { get; set; }

		public AppSectionAttribute(string title, SectionType sectionType, string url, string resourcePath)
			: base(url, resourcePath)
		{
			Title = title;
			Type = sectionType;
		}

		private string cachedTitle;

		public string GetTitle() {

			if (cachedTitle == null)
			{
				if (LangResourceType != null && !string.IsNullOrWhiteSpace(LangResourceKey))
				{
					cachedTitle = LangResourceType.GetProperty(LangResourceKey).GetValue(null) as string;
				}
				
				cachedTitle = cachedTitle ?? Title;
			}

			return cachedTitle;
		}
	}
}