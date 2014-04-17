using System.Net.Http;
using ThinkingHome.Plugins.Listener.Api;

namespace ThinkingHome.Plugins.Listener.Handlers
{
	public interface IListenerHandler
	{
		HttpContent ProcessRequest(HttpRequestParams parameters);
	}
}
