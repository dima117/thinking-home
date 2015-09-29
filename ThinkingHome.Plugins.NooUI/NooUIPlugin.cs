using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.UniUI.Model;
using ThinkingHome.Plugins.UniUI.Widgets;
using ThinkingHome.Plugins.WebUI.Attributes;
using NHibernate;
using NLog;

namespace ThinkingHome.Plugins.NooUI
{
	[Plugin]
	
	[WebWidget("nooui-dimmer", "/widgets/nooui/dimmer-widget.js", "ThinkingHome.Plugins.NooUI.Resources.ui.dimmer-widget.js")]
	[WebWidget("nooui-preset", "/widgets/nooui/preset-widget.js", "ThinkingHome.Plugins.NooUI.Resources.ui.preset-widget.js")]
	[WebWidget("nooui-switcher", "/widgets/nooui/switcher-widget.js", "ThinkingHome.Plugins.NooUI.Resources.ui.switcher-widget.js")]
	[WebWidget("nooui-savepreset", "/widgets/nooui/savepreset-widget.js", "ThinkingHome.Plugins.NooUI.Resources.ui.savepreset-widget.js")]
	
	class NooUIPlugin : PluginBase
	{

	}
}
