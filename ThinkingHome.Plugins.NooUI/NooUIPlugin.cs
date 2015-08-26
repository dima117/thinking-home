using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
<<<<<<< HEAD
using System.ComponentModel.Composition;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Core.Plugins.Utils;
using ThinkingHome.Plugins.UniUI.Model;
using ThinkingHome.Plugins.UniUI.Widgets;
using ThinkingHome.Plugins.WebUI.Attributes;
using NHibernate;
using NLog;
=======
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.WebUI.Attributes;
>>>>>>> NooUI-plugin-(2nd-copy)

namespace ThinkingHome.Plugins.NooUI
{
    [Plugin]
    [WebWidget("NooUISwitcher", "/widgets/NooUISwitcherWidget.js", "ThinkingHome.Plugins.NooUI.Resources.ui.NooUISwitherWidget.js")]
    class NooUIPlugin : PluginBase
    {

    }
<<<<<<< HEAD
    
    
     /*  [Widget("NooUISwitherWidget")]
        public class NooUISwitcherWidget : IWidgetDefinition
        {
            public string DisplayName
            {
                get { return "nooLite (ON/OFF)"; }
            }

            public object GetWidgetData(Widget widget, WidgetParameter[] parameters, ISession session, Logger logger)
            {
                return null;
            }

            public WidgetParameterMetaData[] GetWidgetMetaData(ISession session, Logger logger)
            {
                var fldChannel = new WidgetParameterMetaData
                {
                    DisplayName = "Channel",
                    Name = "channel",
                    Type = WidgetParameterType.Int32,
                    Items = new WidgetSelectItem[32]
                };

                for (var i = 0; i < 32; i++)
                {
                    fldChannel.Items[i] = new WidgetSelectItem(i, i.ToString());
                }

                return new[] { fldChannel };
            }
        }/**/
}
=======
}
>>>>>>> NooUI-plugin-(2nd-copy)
