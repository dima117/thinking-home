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
		/// <summary>
		/// Список панелей и виджетов для редактора
		/// </summary>
		[HttpCommand("/api/uniui/panel/list")]
		public object GetPanelList(HttpRequestParams request)
		{
			Guid dashboardId = request.GetRequiredGuid("id");

			using (var session = Context.OpenSession())
			{
				var info = GetDashboardModel(dashboardId, session);
				var panels = GetPanelListModel(dashboardId, session);

				return new { info, panels };
			}
		}

		[HttpCommand("/api/uniui/panel/create")]
		public object CreatePanel(HttpRequestParams request)
		{
			Guid dashboardId = request.GetRequiredGuid("dashboard");
			string title = request.GetRequiredString("title");

			using (var session = Context.OpenSession())
			{
				var dashboard = session.Load<Dashboard>(dashboardId);

				var panel = new Panel
				{
					Id = Guid.NewGuid(),
					Title = title,
					Dashboard = dashboard,
					SortOrder = int.MaxValue
				};

				session.Save(panel);
				session.Flush();

				return panel.Id;
			}
		}

		[HttpCommand("/api/uniui/panel/rename")]
		public object RenamePanel(HttpRequestParams request)
		{
			Guid id = request.GetRequiredGuid("id");
			string title = request.GetRequiredString("title");

			using (var session = Context.OpenSession())
			{
				var panel = session.Get<Panel>(id);

				if (panel != null)
				{
					panel.Title = title;
					session.Save(panel);
					session.Flush();
				}
			}

			return null;
		}

		[HttpCommand("/api/uniui/panel/delete")]
		public object DeletePanel(HttpRequestParams request)
		{
			Guid id = request.GetRequiredGuid("id");

			using (var session = Context.OpenSession())
			{
				var panel = session.Get<Panel>(id);

				if (panel != null)
				{
					session.Delete(panel);
					session.Flush();
				}
			}

			return null;
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

		private object GetPanelListModel(Guid dashboardId, ISession session)
		{
			var allPanels = session.Query<Panel>()
				.Where(w => w.Dashboard.Id == dashboardId)
				.ToArray();

			var allWidgets = session.Query<Widget>()
				.Where(w => w.Panel.Dashboard.Id == dashboardId)
				.ToArray();

			var list = new List<object>();

			foreach (var panel in allPanels) {

				var widgets = allWidgets
					.Where(w => w.Panel.Id == panel.Id)
					.Select(GetWidgetModel)
					.ToArray();

				var model = new {
					id = panel.Id,
					title = panel.Title,
					widgets = widgets.ToArray(),
					sortOrder = panel.SortOrder
				};

				list.Add(model);
			}

			return list.ToArray();
		}

		private object GetWidgetModel(Widget widget) 
		{
			var typeDisplayName = defs.ContainsKey(widget.TypeAlias)
				? defs[widget.TypeAlias].DisplayName
				: string.Format("Unknown ({0})", widget.TypeAlias);

			var widgetModel = new
			{
				id = widget.Id,
				type = widget.TypeAlias,
				displayName = widget.DisplayName,
				sortOrder = widget.SortOrder,
				typeDisplayName
			};

			return widgetModel;
		}

		#endregion
	}
}
