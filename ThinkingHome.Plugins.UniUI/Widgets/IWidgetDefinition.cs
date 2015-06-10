using NHibernate;
using NLog;
using ThinkingHome.Plugins.UniUI.Model;

namespace ThinkingHome.Plugins.UniUI.Widgets
{
	public interface IWidgetDefinition
	{
		string DisplayName { get; }

		object GetWidgetData(Widget widget, WidgetParameter[] parameters, ISession session, Logger logger);
		
		WidgetParameterMetaData[] GetWidgetMetaData(ISession session, Logger logger);
	}
}
