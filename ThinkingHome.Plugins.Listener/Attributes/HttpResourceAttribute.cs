using System;
using System.Reflection;

namespace ThinkingHome.Plugins.Listener.Attributes
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public abstract class HttpResourceAttribute : Attribute
	{
		private readonly string url;
		private readonly string contentType;

		protected HttpResourceAttribute(string url, string contentType)
		{
			this.url = url;
			this.contentType = contentType;
		}

		public string Url
		{
			get { return url; }
		}

		public string ContentType
		{
			get { return contentType; }
		}

		public abstract byte[] GetContent(Assembly assembly);
	}
}
