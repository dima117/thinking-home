using ThinkingHome.Plugins.Listener.Attributes;

namespace ThinkingHome.Plugins.WebUI.Attributes
{
	public class CssResourceAttribute : HttpEmbeddedResourceAttribute
	{
		public bool AutoLoad { get; set; }

		public CssResourceAttribute(string url, string resourcePath)
			: base(url, resourcePath, "text/css")
		{
		}
	}
}
