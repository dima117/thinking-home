using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using ThinkingHome.Core.Plugins.Utils;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.UniUI.Model;
using ThinkingHome.Plugins.UniUI.Widgets;

namespace ThinkingHome.Plugins.UniUI
{
	public partial class UniUiPlugin
	{
		[HttpCommand("/api/uniui/dashboard/list")]
		public object GetDashboardList(HttpRequestParams request)
		{
			using (var session = Context.OpenSession())
			{
				var allDashboards = session.Query<Dashboard>().ToArray();

				var list = GetDashboardListModel(allDashboards);

				return list;
			}
		}

		[HttpCommand("/api/uniui/dashboard/create")]
		public object CreateDashboard(HttpRequestParams request)
		{
			string title = request.GetRequiredString("title");

			using (var session = Context.OpenSession())
			{
				var dashboard = new Dashboard
				{
					Id = Guid.NewGuid(),
					Title = title,
					SortOrder = int.MaxValue
				};

				session.Save(dashboard);
				session.Flush();

				return dashboard.Id;
			}
		}

		[HttpCommand("/api/uniui/dashboard/delete")]
		public object DeleteDashboard(HttpRequestParams request)
		{
			Guid id = request.GetRequiredGuid("id");

			using (var session = Context.OpenSession())
			{
				var dashboard = session.Get<Dashboard>(id);

				if (dashboard != null)
				{
					session.Delete(dashboard);
					session.Flush();
				}
			}

			return null;
		}

		[HttpCommand("/api/uniui/dashboard/details")]
		public object GetDashboardDetails(HttpRequestParams request)
		{
			Guid? id = request.GetGuid("id");

			using (var session = Context.OpenSession())
			{
				var allDashboards = session.Query<Dashboard>().ToArray();

				if (allDashboards.Any())
				{
					var selected = id.HasValue
						? allDashboards.Single(d => d.Id == id.Value)
						: allDashboards.First();

					var dashboardList = GetDashboardListModel(allDashboards, selected.Id);

					var allPanels = session.Query<Panel>()
						.Where(w => w.Dashboard.Id == selected.Id)
						.ToArray();

					var allWidgets = session.Query<Widget>()
						.Where(w => w.Panel.Dashboard.Id == selected.Id)
						.ToArray();

					var allParameters = session.Query<WidgetParameter>()
						.Where(w => w.Widget.Panel.Dashboard.Id == selected.Id)
						.ToArray();

					var panelList = GetPanelListModel(allPanels, allWidgets, allParameters, session);

					return new
					{
						dashboards = dashboardList,
						panels = panelList
					};
				}

				return null;
			}
		}

		private object[] GetDashboardListModel(Dashboard[] allDashboards, Guid? activeId = null)
		{
			var model = allDashboards
					.Select(x => (object)new
					{
						id = x.Id,
						title = x.Title,
						sortOrder = x.SortOrder,
						active = activeId == x.Id
					})
					.ToArray();

			return model;
		}

		private object[] GetPanelListModel(Panel[] allPanels, Widget[] allWidgets, WidgetParameter[] allParameters, ISession session)
		{
			var list = new List<object>();

			foreach (var panel in allPanels)
			{
				var panelWidgets = allWidgets.Where(w => w.Panel.Id == panel.Id).ToArray();
				var widgetListModel = GetWidgetListModel(panelWidgets, allParameters, session);

				var model = new {
					id = panel.Id,
					title = panel.Title,
					widgets = widgetListModel,
					sortOrder = panel.SortOrder
				};

				list.Add(model);
			}

			return list.ToArray();
		}

		private object[] GetWidgetListModel(Widget[] panelWidgets, WidgetParameter[] allParameters, ISession session)
		{
			var list = new List<object>();

			foreach (var widget in panelWidgets)
			{
				var def = defs.GetValueOrDefault(widget.TypeAlias);

				if (def != null)
				{
					var parameters = allParameters.Where(p => p.Widget.Id == widget.Id).ToArray();

					var model = GetWidgetModel(def, widget, parameters, session);

					if (model != null)
					{
						list.Add(model);
					}
				}
			}
			return list.ToArray();
		}

		/// <summary>
		/// Подготовка модели для отображения виджета в интерфейсе
		/// </summary>
		/// <param name="def">Определение виджета</param>
		/// <param name="widget">Данные об экземпляре виджета</param>
		/// <param name="parameters">Параметры виджета</param>
		/// <param name="session">Сессия БД</param>
		private object GetWidgetModel(IWidgetDefinition def, Widget widget, WidgetParameter[] parameters, ISession session)
		{
			try
			{
				var data = def.GetWidgetData(widget, parameters, session, Logger);

				var model = new
				{
					id = widget.Id,
					type = widget.TypeAlias,
					displayName = widget.DisplayName,
					sortOrder = widget.SortOrder,
					data
				};

				return model;
			}
			catch (Exception ex)
			{
				var message = string.Format(
					"UI widget error (id: {0}, type:{1})", widget.Id, widget.TypeAlias);
				Logger.ErrorException(message, ex);

				return null;
			}
		}
	}
}
