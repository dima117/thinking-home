using System.Net.Http;
using System.Net.Http.Headers;
using ThinkingHome.Plugins.Listener.Api;

namespace ThinkingHome.Plugins.Listener.Handlers
{
	public interface IListenerHandler
	{
		void SetHeaders(HttpResponseHeaders headers);
		HttpContent ProcessRequest(HttpRequestParams parameters);
	}
}
