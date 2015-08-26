using NHibernate;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkingHome.Plugins.UniUI.Model;
using ThinkingHome.Plugins.UniUI.Widgets;

<<<<<<< HEAD
=======

>>>>>>> NooUI-plugin-(2nd-copy)
namespace ThinkingHome.Plugins.NooUI
{
    [Widget("NooUISwitherWidget")]
    class NooUISwitcherWidget : IWidgetDefinition
    {
        public string DisplayName
        {
            get { return "nooLixte (ON/OFF)"; }
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
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> NooUI-plugin-(2nd-copy)
