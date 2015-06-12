using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.UniUI.Model;
using ThinkingHome.Plugins.WebUI.Attributes;

namespace ThinkingHome.Plugins.UniUI
{
	[JavaScriptResource("/webapp/uniui/settings/dashboard-info.js", "ThinkingHome.Plugins.UniUI.Resources.Settings.dashboard-info.js")]
	[JavaScriptResource("/webapp/uniui/settings/dashboard-info-view.js", "ThinkingHome.Plugins.UniUI.Resources.Settings.dashboard-info-view.js")]
	[JavaScriptResource("/webapp/uniui/settings/dashboard-info-model.js", "ThinkingHome.Plugins.UniUI.Resources.Settings.dashboard-info-model.js")]
	[HttpResource("/webapp/uniui/settings/dashboard-info.tpl", "ThinkingHome.Plugins.UniUI.Resources.Settings.dashboard-info.tpl")]
	[HttpResource("/webapp/uniui/settings/dashboard-info-widget.tpl", "ThinkingHome.Plugins.UniUI.Resources.Settings.dashboard-info-widget.tpl")]

	public partial class UniUiPlugin
	{
		[HttpCommand("/api/uniui/dashboard/info")]
		public object GetDashboardInfo(HttpRequestParams request)
		{
			Guid dashboardId = request.GetRequiredGuid("id");

			using (var session = Context.OpenSession())
			{
				var info = GetDashboardInfoModel(dashboardId, session);
				var widgets = GetWidgetListModel(dashboardId, session);

				return new { info, widgets };
			}
		}

		#region private

		private object GetDashboardInfoModel(Guid dashboardId, ISession session)
		{
			var dashboard = session.Query<Dashboard>().First(x => x.Id == dashboardId);

			var model = new
			{
				id = dashboard.Id,
				title = dashboard.Title,
				sortOrder = dashboard.SortOrder
			};

			return model;
		}

		private object[] GetWidgetListModel(Guid dashboardId, ISession session)
		{
			var allWidgets = session.Query<Widget>()
				.Where(w => w.Dashboard.Id == dashboardId)
				.ToArray();

			var list = new List<object>();

			foreach (var widget in allWidgets)
			{
				var typeDisplayName = defs.ContainsKey(widget.TypeAlias)
					? defs[widget.TypeAlias].DisplayName
					: string.Format("Unknown ({0})", widget.TypeAlias);

				var model = new
				{
					id = widget.Id,
					type = widget.TypeAlias,
					displayName = widget.DisplayName,
					typeDisplayName = typeDisplayName,
					sortOrder = widget.SortOrder
				};

				list.Add(model);
			}

			return list.ToArray();
		}

		#endregion
	}
}
