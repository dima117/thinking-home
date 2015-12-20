using System;
using System.Reflection;
using System.Resources;

namespace ThinkingHome.Plugins.Listener.Attributes
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class HttpResourceAttribute : Attribute, IHttpResourceAttribute
	{
		private readonly string url;
		private readonly string resourcePath;
		private readonly string contentType;

		public HttpResourceAttribute(string url, string resourcePath, string contentType = "text/plain")
		{
			this.url = url;
			this.resourcePath = resourcePath;
			this.contentType = contentType;
		}

		public string Url
		{
			get { return url; }
		}
		
		public string ResourcePath
		{
			get { return resourcePath; }
		}

		public string ContentType
		{
			get { return contentType; }
		}

		public virtual byte[] GetContent(Assembly assembly)
		{
			byte[] result;

			using (var stream = assembly.GetManifestResourceStream(resourcePath))
			{
				if (stream != null)
				{
					result = new byte[stream.Length];
					stream.Read(result, 0, result.Length);
				}
				else
				{
					var message = string.Format("resource {0} is not found", resourcePath);
					throw new MissingManifestResourceException(message);
				}
			}

			return result;
		}
	}
}
