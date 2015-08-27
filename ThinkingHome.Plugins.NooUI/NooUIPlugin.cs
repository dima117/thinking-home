using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.WebUI.Attributes;

namespace ThinkingHome.Plugins.NooUI
{
    [Plugin]
    [WebWidget("NooUISwitcher", "/widgets/NooUISwitcherWidget.js", "ThinkingHome.Plugins.NooUI.Resources.ui.NooUISwitherWidget.js")]
    class NooUIPlugin : PluginBase
    {

    }
}