using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using ECM7.Migrator.Framework;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Mapping.ByCode;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Core.Plugins.Utils;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.UniUI.Model;
using ThinkingHome.Plugins.UniUI.Widgets;
using ThinkingHome.Plugins.WebUI.Attributes;

[assembly: MigrationAssembly("ThinkingHome.Plugins.UniUI")]

namespace ThinkingHome.Plugins.UniUI
{
	[Plugin]

	[AppSection("Dashboard list", SectionType.System, "/webapp/uniui/settings/dashboard-list.js", "ThinkingHome.Plugins.UniUI.Resources.Settings.dashboard-list.js")]
	[JavaScriptResource("/webapp/uniui/settings/dashboard-list-view.js", "ThinkingHome.Plugins.UniUI.Resources.Settings.dashboard-list-view.js")]
	[JavaScriptResource("/webapp/uniui/settings/dashboard-list-model.js", "ThinkingHome.Plugins.UniUI.Resources.Settings.dashboard-list-model.js")]
	[HttpResource("/webapp/uniui/settings/dashboard-list.tpl", "ThinkingHome.Plugins.UniUI.Resources.Settings.dashboard-list.tpl")]
	[HttpResource("/webapp/uniui/settings/dashboard-list-item.tpl", "ThinkingHome.Plugins.UniUI.Resources.Settings.dashboard-list-item.tpl")]

	[JavaScriptResource("/webapp/uniui/settings/dashboard-info.js", "ThinkingHome.Plugins.UniUI.Resources.Settings.dashboard-info.js")]
	[JavaScriptResource("/webapp/uniui/settings/dashboard-info-view.js", "ThinkingHome.Plugins.UniUI.Resources.Settings.dashboard-info-view.js")]
	[JavaScriptResource("/webapp/uniui/settings/dashboard-info-model.js", "ThinkingHome.Plugins.UniUI.Resources.Settings.dashboard-info-model.js")]
	[HttpResource("/webapp/uniui/settings/dashboard-info.tpl", "ThinkingHome.Plugins.UniUI.Resources.Settings.dashboard-info.tpl")]
	[HttpResource("/webapp/uniui/settings/dashboard-info-widget.tpl", "ThinkingHome.Plugins.UniUI.Resources.Settings.dashboard-info-widget.tpl")]

	public class UniUiPlugin : PluginBase
	{
		public override void InitDbModel(ModelMapper mapper)
		{
			mapper.Class<Dashboard>(cfg => cfg.Table("UniUI_Dashboard"));
			mapper.Class<Widget>(cfg => cfg.Table("UniUI_Widget"));
			mapper.Class<WidgetParameter>(cfg => cfg.Table("UniUI_WidgetParameter"));
		}

		#region available widgets

		private readonly InternalDictionary<IWidgetDefinition> definitions = new InternalDictionary<IWidgetDefinition>();

		[ImportMany("ABD9D425-5836-4DC5-88B6-222CD7A658CA")]
		public Lazy<IWidgetDefinition, IWidgetAttribute>[] WidgetDefinitions { get; set; }

		public override void InitPlugin()
		{
			foreach (var def in WidgetDefinitions)
			{
				Logger.Info("Register UI widget : '{0}'", def.Metadata.TypeAlias);
				definitions.Register(def.Metadata.TypeAlias, def.Value);
			}
		}

		#endregion

		#region dashboard api

		[HttpCommand("/api/uniui/dashboard/list")]
		public object GetDashboardList(HttpRequestParams request)
		{
			using (var session = Context.OpenSession())
			{
				var list = session.Query<Dashboard>().ToList();

				var model = list
					.Select(x => new
					{
						id = x.Id,
						title = x.Title,
						sortOrder = x.SortOrder
					})
					.ToList();

				return model;
			}
		}

		[HttpCommand("/api/uniui/dashboard/add")]
		public object AddDashboard(HttpRequestParams request)
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

		[HttpCommand("/api/uniui/dashboard/rename")]
		public object RenameDashboard(HttpRequestParams request)
		{
			Guid id = request.GetRequiredGuid("id");
			string title = request.GetRequiredString("title");

			using (var session = Context.OpenSession())
			{
				var dashboard = session.Get<Dashboard>(id);

				if (dashboard != null)
				{
					dashboard.Title = title;

					session.Save(dashboard);
					session.Flush();
				}
			}

			return null;
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

		[HttpCommand("/api/uniui/dashboard/move")]
		public object MoveDashboard(HttpRequestParams request)
		{
			Guid id = request.GetRequiredGuid("id");
			bool moveUp = request.GetRequiredBool("up");

			using (var session = Context.OpenSession())
			{
				var list = session.Query<Dashboard>().OrderBy(d => d.SortOrder).ToList();
				var index = list.FindIndex(d => d.Id == id);

				if (index >= 0)
				{
					var otherIndex = moveUp ? index - 1 : index + 1;

					if (otherIndex >= 0 && otherIndex < list.Count)
					{
						var tmp = list[otherIndex];
						list[otherIndex] = list[index];
						list[index] = tmp;

						for (int i = 0; i < list.Count; i++)
						{
							list[i].SortOrder = i;
							session.Save(list[i]);
						}

						session.Flush();
					}
				}
			}

			return null;
		}

		#endregion

		#region widget api

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

		[HttpCommand("/api/uniui/widget/info")]
		public object GetWidgetInfo(HttpRequestParams request)
		{
			Debugger.Launch();
			// todo: нужен рефакторинг
			Guid widgetId = request.GetRequiredGuid("id");

			using (var session = Context.OpenSession())
			{
				var widget = session.Query<Widget>().Single(x => x.Id == widgetId);
				var widgetParams = session
					.Query<WidgetParameter>()
					.Where(x => x.Widget.Id == widgetId)
					.ToList();

				var def = definitions[widget.TypeAlias];

				var metaParams = def.GetWidgetMetaData(session, Logger);

				var list = new List<object>();

				foreach (var obj in metaParams)
				{
					object value = null;

					WidgetParameter p = widgetParams.FirstOrDefault(wp => wp.Name == obj.Name);

					if (p != null)
					{
						switch (obj.Type)
						{
							case WidgetParameterType.Guid:
								value = p.ValueGuid;
								break;
							case WidgetParameterType.Int32:
								value = p.ValueInt;
								break;
							case WidgetParameterType.String:
								value = p.ValueString;
								break;
						}
					}

					var items = obj.Items.Select(i => new { id = i.Id, diaplsyName = i.DisplayName }).ToArray();

					var parameterModel = new
					{
						name = obj.Name,
						displayName = obj.DisplayName,
						type = obj.Type.ToString().ToLower(),
						value,
						items
					};

					list.Add(parameterModel);
				}

				return new
				{
					id = widget.Id,
					dashboardId = widget.Dashboard.Id,
					type = widget.TypeAlias,
					displayName = widget.DisplayName,
					parameters = list.ToArray()
				};
			}
		}

		#endregion

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

			var allParameters = session.Query<WidgetParameter>()
				.Where(p => p.Widget.Dashboard.Id == dashboardId)
				.ToArray();

			var list = new List<object>();

			foreach (var widget in allWidgets)
			{
				var widgetParams = allParameters.Where(p => p.Widget.Id == widget.Id).ToArray();

				var model = GetWidgetModel(session, widget, widgetParams);
				list.Add(model);
			}

			return list.ToArray();
		}

		private object GetWidgetModel(ISession session, Widget widget, WidgetParameter[] widgetParams)
		{
			if (!definitions.ContainsKey(widget.TypeAlias))
			{
				var exceptionWidget = CreateExceptionWidget("Widget \"{0}\" has an invalid type \"{1}\"", widget);
				return exceptionWidget;
			}

			try
			{
				var def = definitions[widget.TypeAlias];
				var data = def.GetWidgetData(widget, widgetParams, session, Logger);

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
				var exceptionWidget = CreateExceptionWidget(ex.Message, widget);
				return exceptionWidget;
			}
		}

		private object CreateExceptionWidget(string message, Widget problem)
		{
			var msg = string.Format(message, problem.DisplayName, problem.TypeAlias);

			Logger.Warn(msg);

			return new
			{
				id = problem.Id,
				type = "exception",
				displayName = msg,
				sortOrder = problem.SortOrder
			};
		}

		#endregion
	}
}
