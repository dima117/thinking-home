using System;
using System.Net.Http;
using System.Net.Http.Headers;
using ThinkingHome.Plugins.Listener.Api;

namespace ThinkingHome.Plugins.Listener.Handlers
{
	public class DynamicFileListenerHandler : ListenerHandler
	{
		private readonly string contentType;
		private readonly Func<HttpRequestParams, byte[]> action;

		public DynamicFileListenerHandler(Func<HttpRequestParams, byte[]> action, string contentType)
		{
			if (action == null)
			{
				throw new NullReferenceException();
			}

			this.action = action;
			this.contentType = contentType;
		}

		public override bool CacheResponse
		{
			get { return true; }
		}

		public override HttpContent GetContent(HttpRequestParams parameters)
		{
			byte[] result = action(parameters);

			HttpContent content = new ByteArrayContent(result);
			content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

			return content;
		}
	}
}
