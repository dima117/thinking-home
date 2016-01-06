using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.WebUI.Attributes;

namespace ThinkingHome.Plugins.NooUI
{
	[Plugin]
	
	[WebWidget("nooui-dimmer", "/widgets/nooui/dimmer-widget.js", "ThinkingHome.Plugins.NooUI.Resources.ui.dimmer-widget.js")]
	[WebWidget("nooui-preset", "/widgets/nooui/preset-widget.js", "ThinkingHome.Plugins.NooUI.Resources.ui.preset-widget.js")]
	[WebWidget("nooui-preset-loader", "/widgets/nooui/preset-loader-widget.js", "ThinkingHome.Plugins.NooUI.Resources.ui.preset-loader-widget.js")]
	[WebWidget("nooui-switcher", "/widgets/nooui/switcher-widget.js", "ThinkingHome.Plugins.NooUI.Resources.ui.switcher-widget.js")]

	// i18n
	[HttpI18NResource("/webapp/nooui/lang.json", "ThinkingHome.Plugins.NooUI.Lang.NooUiLang")]

	public class NooUIPlugin : PluginBase
	{

	}
}
