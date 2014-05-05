using System;
using ThinkingHome.Plugins.Listener.Handlers;

namespace ThinkingHome.Plugins.WebUI.Attributes
{
	public class JavaScriptResourceAttribute : HttpResourceAttribute
	{
		public JavaScriptResourceAttribute(string url, string resourcePath)
			: base(url, resourcePath, "text/javascript")
		{
		}

		public string GetModulePath()
		{
			string url = (Url ?? string.Empty).Trim().Trim('/');
			return url.EndsWith(".js", StringComparison.InvariantCultureIgnoreCase)
				? url.Substring(0, url.Length - 3)
				: url;
		}
	}
}