using NHibernate;
using NLog;
using ThinkingHome.Plugins.UniUI.Model;
using ThinkingHome.Plugins.UniUI.Widgets;

namespace ThinkingHome.Plugins.Tmp
{
	[Widget("tmp-camera")]
	public class CameraWidgetDefinition : IWidgetDefinition
	{
		public string DisplayName {
			get { return "Camera image"; }
		}
		public object GetWidgetData(Widget widget, WidgetParameter[] parameters, ISession session, Logger logger)
		{
			return null;
		}

		public WidgetParameterMetaData[] GetWidgetMetaData(ISession session, Logger logger)
		{
			return new WidgetParameterMetaData[0];
		}
	}
}
