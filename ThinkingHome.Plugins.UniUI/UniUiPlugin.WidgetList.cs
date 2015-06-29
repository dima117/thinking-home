using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.UniUI.Model;

namespace ThinkingHome.Plugins.UniUI
{
	public partial class UniUiPlugin
	{
		[HttpCommand("/api/uniui/widget/list")]
		public object GetWidgetList(HttpRequestParams request)
		{
			Guid dashboardId = request.GetRequiredGuid("id");

			using (var session = Context.OpenSession())
			{
				var info = GetDashboardModel(dashboardId, session);
				var widgets = GetWidgetListModel(dashboardId, session);

				return new { info, widgets };
			}
		}

		#region private

		private object GetDashboardModel(Guid dashboardId, ISession session)
		{
			var dashboard = session.Query<Dashboard>().Single(x => x.Id == dashboardId);

			var types = defs
				.Select(x => new { id = x.Key, name = x.Value.DisplayName })
				.ToArray();

			var model = new
			{
				id = dashboard.Id,
				title = dashboard.Title,
				types
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
					sortOrder = widget.SortOrder,
					typeDisplayName
				};

				list.Add(model);
			}

			return list.ToArray();
		}

		#endregion
	}
}
