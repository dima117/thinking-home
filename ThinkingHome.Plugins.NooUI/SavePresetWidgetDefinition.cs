﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkingHome.Plugins.UniUI.Model;
using ThinkingHome.Plugins.UniUI.Widgets;
using NHibernate;
using NLog;

/* TODO: 
 * Вынести адрес api в доступное для настройки пользователем место.
 */

namespace ThinkingHome.Plugins.NooUI
{
	[Widget("nooui-savepreset")]
	class SavePresetWidgetDefinition : IWidgetDefinition
	{
		private const string PARAM_CHANNEL = "channel";
		private const string PARAM_CHANNEL_DISPLAY_NAME = "Channel";

		public string DisplayName
		{
			get { return "NooUI SavePreset"; }
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
				DisplayName = PARAM_CHANNEL_DISPLAY_NAME,
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
