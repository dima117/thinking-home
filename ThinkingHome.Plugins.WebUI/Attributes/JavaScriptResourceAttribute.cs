using ThinkingHome.Plugins.Listener.Handlers;

namespace ThinkingHome.Plugins.WebUI.Attributes
{
	public class JavaScriptResourceAttribute : HttpResourceAttribute
	{
		public JavaScriptResourceAttribute(string url, string resourcePath)
			: base(url, resourcePath, "text/javascript")
		{
		}
	}
}