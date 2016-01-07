using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Util;
using ThinkingHome.Core.Plugins.Utils;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.UniUI.Model;
using ThinkingHome.Plugins.UniUI.Widgets;

namespace ThinkingHome.Plugins.UniUI
{
	public partial class UniUiPlugin
	{
		#region public

		[HttpCommand("/api/uniui/widget/create")]
		public object EditorCreateWidget(HttpRequestParams request)
		{
			var panelId = request.GetRequiredGuid("panel");
			var type = request.GetRequiredString("type");

			using (var session = Context.OpenSession())
			{
				var panel = session.Query<Panel>().Fetch(p => p.Dashboard).Single(x => x.Id == panelId);
				var model = GetEditorModel(type, panel, session);

				return new
				{
					info = model.Item1,
					fields = model.Item2
				};
			}
		}

		[HttpCommand("/api/uniui/widget/edit")]
		public object EditorOpenWidget(HttpRequestParams request)
		{
			var id = request.GetRequiredGuid("id");

			using (var session = Context.OpenSession())
			{
				var widget = session.Query<Widget>()
					.Fetch(a => a.Panel)
					.ThenFetch(p => p.Dashboard)
					.Single(x => x.Id == id);

				var model = GetEditorModel(widget.TypeAlias, widget.Panel, session);

				var parameters = session.Query<WidgetParameter>()
					.Where(x => x.Widget.Id == id)
					.ToArray();

				FillEditorModel(model, widget, parameters);

				return new
				{
					info = model.Item1,
					fields = model.Item2
				};
			}
		}

		[HttpCommand("/api/uniui/widget/save")]
		public object EditorSaveWidget(HttpRequestParams request)
		{
			using (var session = Context.OpenSession())
			{
				using (var tran = session.BeginTransaction())
				{
					try
					{
						var widget = SaveWidget(request, session);

						SaveWidgetFields(widget, request, session);

						tran.Commit();
					}
					catch
					{
						tran.Rollback();
						throw;
					}
				}
			}

			return null;
		}

		[HttpCommand("/api/uniui/widget/delete")]
		public object EditorDeleteWidget(HttpRequestParams request)
		{
			Guid id = request.GetRequiredGuid("id");

			using (var session = Context.OpenSession())
			{
				var widget = session.Get<Widget>(id);

				if (widget != null)
				{
					session.Delete(widget);
					session.Flush();
				}
			}

			return null;
		}

		#endregion

		#region private: save

		private Widget SaveWidget(HttpRequestParams request, ISession session)
		{
			var id = request.GetGuid("id");

			var widget = id.HasValue
				? session.Query<Widget>().Single(x => x.Id == id)
				: CreateWidget(request, session);

			widget.DisplayName = request.GetString("displayName") ?? "noname";

			session.Save(widget);
			session.Flush();

			return widget;
		}

		private Widget CreateWidget(HttpRequestParams request, ISession session)
		{
			var type = request.GetRequiredString("type");
			var panelId = request.GetRequiredGuid("panelId");

			var panel = session.Query<Panel>().Single(x => x.Id == panelId);

			var created = new Widget
			{
				Id = Guid.NewGuid(),
				Panel = panel,
				TypeAlias = type,
				SortOrder = int.MaxValue
			};

			return created;
		}

		private void SaveWidgetFields(Widget widget, HttpRequestParams request, ISession session)
		{
			var def = defs.GetValueOrDefault(widget.TypeAlias);

			if (def != null)
			{
				session.Query<WidgetParameter>()
					.Where(p => p.Widget.Id == widget.Id)
					.ForEach(session.Delete);

				session.Flush();

				var fields = def.GetWidgetMetaData(session, Logger) 
					?? new WidgetParameterMetaData[0];

				var json = request.GetRequiredString("json");
				var values = Extensions.FromJson<Dictionary<string, string>>(json);

				foreach (var field in fields)
				{
					var value = values.GetValueOrDefault(field.Name);

					CreateParameter(session, widget, field, value);
				}

				session.Flush();
			}
		}

		private static void CreateParameter(ISession session, Widget widget, WidgetParameterMetaData field, string value)
		{
			var p = new WidgetParameter
			{
				Id = Guid.NewGuid(),
				Widget = widget,
				Name = field.Name
			};

			switch (field.Type)
			{
				case WidgetParameterType.String:
					p.ValueString = value;
					break;
				case WidgetParameterType.Guid:
					p.ValueGuid = value.ParseGuid();
					break;
				case WidgetParameterType.Int32:
					p.ValueInt = value.ParseInt();
					break;
			}

			session.Save(p);
		}

		#endregion

		#region private: load

		private Tuple<EditorModel, EditorParameterModel[]> GetEditorModel(
			string type, Panel panel, ISession session)
		{
			if (!defs.ContainsKey(type))
			{
				throw new Exception(string.Format("invalid widget type {0}", type));
			}

			var def = defs[type];

			var metaData = def.GetWidgetMetaData(session, Logger) 
				?? new WidgetParameterMetaData[0];

			var parameters = metaData
						.Select(GetEditorParameterModel)
						.ToArray();

			var model = new EditorModel
			{
				typeDisplayName = def.DisplayName,
				panelId = panel.Id,
				panelTitle = panel.Title,
				dashboardId = panel.Dashboard.Id,
				dashboardTitle = panel.Dashboard.Title,
				type = type
			};

			return new Tuple<EditorModel, EditorParameterModel[]>(model, parameters);
		}

		private EditorParameterModel GetEditorParameterModel(WidgetParameterMetaData parameter)
		{
			var items = parameter.Items == null
				? null
				: parameter.Items
					.Select(i => new { id = i.Id, name = i.DisplayName })
					.ToArray();

			var ptype = parameter.Type.ToString().ToLower();

			var pmodel = new EditorParameterModel
			{
				name = parameter.Name,
				displayName = parameter.DisplayName,
				type = ptype,
				items = items
			};

			return pmodel;
		}

		private void FillEditorModel(Tuple<EditorModel, EditorParameterModel[]> model, Widget widget, WidgetParameter[] parameters)
		{
			model.Item1.id = widget.Id;
			model.Item1.displayName = widget.DisplayName;

			foreach (var parameter in parameters)
			{
				var pmodel = model.Item2.FirstOrDefault(p => p.name == parameter.Name);

				if (pmodel != null)
				{
					switch (pmodel.type)
					{
						case "guid":
							pmodel.value = parameter.ValueGuid;
							break;
						case "int32":
							pmodel.value = parameter.ValueInt;
							break;
						case "string":
							pmodel.value = parameter.ValueString;
							break;
					}
				}
			}
		}

		#endregion

		#region private: model

		private class EditorModel
		{
			public Guid? id;
			public string displayName;
			public string typeDisplayName;
			public string type;
			public Guid panelId;
			public string panelTitle;
			public Guid dashboardId;
			public string dashboardTitle;
		}

		private class EditorParameterModel
		{
			public string name;
			public string displayName;
			public string type;
			public object items;
			public object value;
		}

		#endregion
	}
}
