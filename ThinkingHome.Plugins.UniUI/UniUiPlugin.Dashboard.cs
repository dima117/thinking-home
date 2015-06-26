using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.WebUI.Attributes;

namespace ThinkingHome.Plugins.UniUI
{
	[JavaScriptResource("/webapp/uniui/ui/dashboard.js", "ThinkingHome.Plugins.UniUI.Resources.UI.dashboard.js")]
	[JavaScriptResource("/webapp/uniui/ui/dashboard-view.js", "ThinkingHome.Plugins.UniUI.Resources.UI.dashboard-view.js")]
	[JavaScriptResource("/webapp/uniui/ui/dashboard-model.js", "ThinkingHome.Plugins.UniUI.Resources.UI.dashboard-model.js")]
	[HttpResource("/webapp/uniui/ui/dashboard.tpl", "ThinkingHome.Plugins.UniUI.Resources.UI.dashboard.tpl")]

	public partial class UniUiPlugin
	{

	}
}
