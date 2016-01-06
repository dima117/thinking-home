using System.Linq;
using ThinkingHome.Plugins.UniUI.Model;
using ThinkingHome.Plugins.UniUI.Widgets;
using NHibernate;
using NLog;
using ThinkingHome.Plugins.NooUI.Lang;

namespace ThinkingHome.Plugins.NooUI
{
	[Widget("nooui-dimmer")]
	class DimmerWidgetDefinition : IWidgetDefinition
	{
		private const string PARAM_CHANNEL = "channel";

		public string DisplayName
		{
			get { return NooUiLang.NooUI_Dimmer; }
		}

		public object GetWidgetData(Widget widget, WidgetParameter[] parameters, ISession session, Logger logger)
		{
			var wChannel = parameters.First(p => p.Name == PARAM_CHANNEL).ValueInt;
			return new
			{
				channel = wChannel
			};
		}

		public WidgetParameterMetaData[] GetWidgetMetaData(ISession session, Logger logger)
		{
			var fldChannel = new WidgetParameterMetaData
			{
				Name = PARAM_CHANNEL,
				DisplayName = NooUiLang.Channel,
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
}
