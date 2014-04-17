using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Listener.Handlers;

namespace ThinkingHome.Plugins.WebUI
{
	[Plugin]
	[HttpResource("/", "ThinkingHome.Plugins.WebUI.Resources.Index.html", "text/html")]
    public class WebUIPlugin : Plugin
    {
    }
}
