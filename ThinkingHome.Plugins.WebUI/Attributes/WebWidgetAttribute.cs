namespace ThinkingHome.Plugins.WebUI.Attributes
{
	public class WebWidgetAttribute : JavaScriptResourceAttribute
	{
		public string Type { get; set; }

		public WebWidgetAttribute(string type, string url, string resourcePath)
			: base(url, resourcePath)
		{
			Type = type;
		}
	}
}