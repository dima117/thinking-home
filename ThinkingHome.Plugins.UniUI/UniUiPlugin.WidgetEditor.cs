using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.UniUI.Model;
using ThinkingHome.Plugins.UniUI.Widgets;

namespace ThinkingHome.Plugins.UniUI
{
	public partial class UniUiPlugin
	{
		#region public

		[HttpCommand("/api/uniui/editor/create")]
		public object EditorCreateWidget(HttpRequestParams request)
		{
			var dashboardId = request.GetRequiredGuid("dashboard");
			var type = request.GetRequiredString("type");

			using (var session = Context.OpenSession())
			{
				var model = GetEditorModel(type, dashboardId, session);

				return model;
			}
		}

		[HttpCommand("/api/uniui/editor/open")]
		public object EditorOpenWidget(HttpRequestParams request)
		{
			var id = request.GetRequiredGuid("id");

			using (var session = Context.OpenSession())
			{
				var widget = session.Query<Widget>().Single(x => x.Id == id);

				var model = GetEditorModel(widget.TypeAlias, widget.Dashboard.Id, session);

				var parameters = session.Query<WidgetParameter>()
					.Where(x => x.Widget.Id == id)
					.ToArray();

				FillEditorModel(model, widget, parameters);

				return model;
			}
		}

		#endregion

		#region private methods

		private EditorModel GetEditorModel(string type, Guid dashboardId, ISession session)
		{
			if (!definitions.ContainsKey(type))
			{
				throw new Exception(string.Format("invalid widget type {0}", type));
			}

			var def = definitions[type];

			var parameters = def
						.GetWidgetMetaData(session, Logger)
						.Select(GetEditorParameterModel)
						.ToArray();

			var model = new EditorModel
			{
				dashboardId = dashboardId,
				type = type,
				parameters = parameters
			};

			return model;
		}

		private EditorParameterModel GetEditorParameterModel(WidgetParameterMetaData parameter)
		{
			var items = parameter.Items == null
				? null
				: parameter.Items
					.Select(i => new { id = i.Id, diaplsyName = i.DisplayName })
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

		private void FillEditorModel(EditorModel model, Widget widget, WidgetParameter[] parameters)
		{
			model.id = widget.Id;
			model.displayName = widget.DisplayName;

			foreach (var parameter in parameters)
			{
				var pmodel = model.parameters.FirstOrDefault(p => p.name == parameter.Name);

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
			public Guid dashboardId;
			public string type;
			public string displayName;
			public EditorParameterModel[] parameters;
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
