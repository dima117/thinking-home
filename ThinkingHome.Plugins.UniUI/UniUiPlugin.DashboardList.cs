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
	}
}
