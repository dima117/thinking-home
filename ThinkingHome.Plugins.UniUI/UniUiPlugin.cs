using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.UniUI.Model;

namespace ThinkingHome.Plugins.UniUI
{
	[Plugin]
	public class UniUiPlugin : PluginBase
	{
		[HttpCommand("/api/uniui/dashboard/list")]
		public object GetList(HttpRequestParams request)
		{
			Guid? id = request.GetGuid("id");

			using (var session = Context.OpenSession())
			{
				var list = LoadDashboardList(session);
				var selected = list.FirstOrDefault(d => d.Id == id)
							   ?? list.FirstOrDefault();

				var elements = LoadDashboardElements(selected, session);

				var listModel = CreateListModel(list, selected);
				var elementsModel = CreateElementsModel(selected, elements);

				return new
				{
					list = listModel,
					elements = elementsModel
				};
			}
		}

		[HttpCommand("/api/uniui/dashboard/elements")]
		public object GetElements(HttpRequestParams request)
		{
			Guid id = request.GetRequiredGuid("id");

			using (var session = Context.OpenSession())
			{
				var selected = session.Query<Dashboard>().Single(x => x.Id == id);
				var elements = LoadDashboardElements(selected, session);
				var model = CreateElementsModel(selected, elements);

				return model;
			}
		}

		#region database

		private static List<Dashboard> LoadDashboardList(ISession session)
		{
			return session.Query<Dashboard>()
				.OrderBy(x => x.SortOrder)
				.ToList();
		}

		private List<DashboardElement> LoadDashboardElements(Dashboard selected, ISession session)
		{
			return session.Query<DashboardElement>()
				.Where(el => el.Dashboard == selected)
				.OrderBy(el => el.SortOrder)
				.ToList();
		}

		#endregion

		#region build model

		private object CreateListModel(List<Dashboard> list, Dashboard selected)
		{
			return list
				.Select(x => new
					{
						id = x.Id,
						title = x.Title,
						selected = selected != null && x.Id == selected.Id
					})
				.ToList();
		}

		private object CreateElementsModel(Dashboard selected, List<DashboardElement> elements)
		{
			if (selected == null)
			{
				return null;
			}

			var list = elements
				.Select(el => new
					{
						id = el.Id,
						type = el.TypeAlias,
						data = el.GetParameters()
					})
				.ToList();

			return new
			{
				id = selected.Id,
				title = selected.Title,
				elements = list
			};
		}

		#endregion
	}
}
