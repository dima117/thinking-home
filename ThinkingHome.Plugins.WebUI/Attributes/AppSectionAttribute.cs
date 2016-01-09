using System;
using System.Resources;

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

		public string GetTitle()
		{
			if (cachedTitle == null)
			{
				if (LangResourceType != null && !string.IsNullOrWhiteSpace(LangResourceKey))
				{
					var resourceManager = new ResourceManager(LangResourceType.FullName, LangResourceType.Assembly);
					cachedTitle = resourceManager.GetString(LangResourceKey);
				}
				
				cachedTitle = cachedTitle ?? Title;
			}

			return cachedTitle;
		}
	}
}