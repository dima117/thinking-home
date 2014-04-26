namespace ThinkingHome.Plugins.WebUI.Attributes
{
	public class AppSectionAttribute : JavaScriptResourceAttribute
	{
		public string Title { get; set; }

		public AppSectionAttribute(string title, string url, string resourcePath)
			: base(url, resourcePath)
		{
			Title = title;
		}
	}
}