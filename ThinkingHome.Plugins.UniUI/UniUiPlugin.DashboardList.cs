using System;
using System.Linq;
using NHibernate.Linq;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.UniUI.Model;
using ThinkingHome.Plugins.WebUI.Attributes;

namespace ThinkingHome.Plugins.UniUI
{
	[AppSection("Dashboard list", SectionType.System, "/webapp/uniui/settings/dashboard-list.js", "ThinkingHome.Plugins.UniUI.Resources.Settings.dashboard-list.js")]
	[JavaScriptResource("/webapp/uniui/settings/dashboard-list-view.js", "ThinkingHome.Plugins.UniUI.Resources.Settings.dashboard-list-view.js")]
	[JavaScriptResource("/webapp/uniui/settings/dashboard-list-model.js", "ThinkingHome.Plugins.UniUI.Resources.Settings.dashboard-list-model.js")]
	[HttpResource("/webapp/uniui/settings/dashboard-list.tpl", "ThinkingHome.Plugins.UniUI.Resources.Settings.dashboard-list.tpl")]
	[HttpResource("/webapp/uniui/settings/dashboard-list-item.tpl", "ThinkingHome.Plugins.UniUI.Resources.Settings.dashboard-list-item.tpl")]

	public partial class UniUiPlugin
	{
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
	}
}
