using System;

namespace ThinkingHome.Plugins.Listener.Attributes
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public class HttpResourceAttribute : Attribute
	{
		public readonly string Url;
		public readonly string ResourcePath;
		public readonly string ContentType;

		public HttpResourceAttribute(string url, string resourcePath, string contentType = "text/plain")
		{
			Url = url;
			ResourcePath = resourcePath;
			ContentType = contentType;
		}
	}
}
