using System.Net.Http;
using System.Net.Http.Headers;
using ThinkingHome.Plugins.Listener.Api;

namespace ThinkingHome.Plugins.Listener.Handlers
{
	public abstract class ListenerHandler
	{
		public virtual bool CacheResponse
		{
			get { return true; }
		}

		public abstract HttpContent GetContent(HttpRequestParams parameters);

		public HttpResponseMessage ProcessRequest(HttpRequestParams parameters)
		{
			var content = GetContent(parameters);

			var response = new HttpResponseMessage { Content = content };

			if (!CacheResponse)
			{
				response.Headers.CacheControl = new CacheControlHeaderValue { NoStore = true, NoCache = true };
				response.Headers.Pragma.Add(new NameValueHeaderValue("no-cache"));
			}

			return response;
		}
	}
}
