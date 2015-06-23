using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.UniUI.Model;
using ThinkingHome.Plugins.UniUI.Widgets;
using ThinkingHome.Plugins.WebUI.Attributes;

namespace ThinkingHome.Plugins.UniUI
{
	[JavaScriptResource("/webapp/uniui/settings/widget-editor.js", "ThinkingHome.Plugins.UniUI.Resources.Settings.widget-editor.js")]
	[JavaScriptResource("/webapp/uniui/settings/widget-editor-view.js", "ThinkingHome.Plugins.UniUI.Resources.Settings.widget-editor-view.js")]
	[JavaScriptResource("/webapp/uniui/settings/widget-editor-model.js", "ThinkingHome.Plugins.UniUI.Resources.Settings.widget-editor-model.js")]
	[HttpResource("/webapp/uniui/settings/widget-editor.tpl", "ThinkingHome.Plugins.UniUI.Resources.Settings.widget-editor.tpl")]
	[HttpResource("/webapp/uniui/settings/widget-editor-field.tpl", "ThinkingHome.Plugins.UniUI.Resources.Settings.widget-editor-field.tpl")]

	public partial class UniUiPlugin
	{
		#region public

		[HttpCommand("/api/uniui/widget/create")]
		public object EditorCreateWidget(HttpRequestParams request)
		{
			var dashboardId = request.GetRequiredGuid("dashboard");
			var type = request.GetRequiredString("type");

			using (var session = Context.OpenSession())
			{
				var dashboard = session.Query<Dashboard>().Single(x => x.Id == dashboardId);
				var model = GetEditorModel(type, dashboard.Id, dashboard.Title, session);

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
								.Fetch(a => a.Dashboard)
								.Single(x => x.Id == id);

				var model = GetEditorModel(widget.TypeAlias, widget.Dashboard.Id, widget.Dashboard.Title, session);

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
			return null;
		}

		#endregion

		#region private: methods

		private Tuple<EditorModel, EditorParameterModel[]> GetEditorModel(
			string type, Guid dashboardId, string dashboardTitle, ISession session)
		{
			if (!defs.ContainsKey(type))
			{
				throw new Exception(string.Format("invalid widget type {0}", type));
			}

			var def = defs[type];

			var parameters = def
						.GetWidgetMetaData(session, Logger)
						.Select(GetEditorParameterModel)
						.ToArray();

			var model = new EditorModel
			{
				typeDisplayName = def.DisplayName,
				dashboardId = dashboardId,
				dashboardTitle = dashboardTitle,
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
