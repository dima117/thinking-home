using System.Reflection;
using System.Resources;

namespace ThinkingHome.Plugins.Listener.Attributes
{
	
	public class HttpEmbeddedResourceAttribute : HttpResourceAttribute
	{
		private readonly string resourcePath;

		public HttpEmbeddedResourceAttribute(string url, string resourcePath, string contentType = "text/plain")
			:base(url, contentType)
		{
			this.resourcePath = resourcePath;
		}
		
		public string ResourcePath
		{
			get { return resourcePath; }
		}
		
		public override byte[] GetContent(Assembly assembly)
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
