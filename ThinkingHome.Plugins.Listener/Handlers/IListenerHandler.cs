using System.Threading.Tasks;
using Microsoft.Owin;

namespace ThinkingHome.Plugins.Listener.Handlers
{
	public interface IListenerHandler
	{
		Task ProcessRequest(OwinRequest request);
	}
}
