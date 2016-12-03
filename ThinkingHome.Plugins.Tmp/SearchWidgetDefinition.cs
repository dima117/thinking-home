using System;
using NHibernate;
using NLog;
using ThinkingHome.Plugins.UniUI.Model;
using ThinkingHome.Plugins.UniUI.Widgets;

namespace ThinkingHome.Plugins.Tmp
{
    [Widget("search")]
    public class SearchWidgetDefinition : IWidgetDefinition
    {
        public string DisplayName
        {
            get { return "Search in web"; }
        }

        public object GetWidgetData(Widget widget, WidgetParameter[] parameters, ISession session, Logger logger)
        {
            throw new NotImplementedException();
        }

        public WidgetParameterMetaData[] GetWidgetMetaData(ISession session, Logger logger)
        {
            var paramEngine = new WidgetParameterMetaData
            {
                DisplayName = "Search engine",
                Name = "engine",
                Type = WidgetParameterType.Int32,
                Items = new WidgetSelectItem[] {
                     new WidgetSelectItem(1, "Yandex"),
                     new WidgetSelectItem(2, "Google")
                }
            };

            var paramCount = new WidgetParameterMetaData
            {
                DisplayName = "Search result count",
                Name = "count",
                Type = WidgetParameterType.Int32
            };

            return new[] { paramEngine, paramCount };
        }
    }
}
