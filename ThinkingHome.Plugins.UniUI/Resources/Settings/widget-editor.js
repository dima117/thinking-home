define(['app', 'common',
		'webapp/uniui/settings/widget-editor-model.js',
		'webapp/uniui/settings/widget-editor-view.js'
],
	function (application, common, models, views) {

		var api = {

			initEditor: function (data) {

				var view = new views.WidgetEditorView({
					model: data.info,
					collection: data.fields
				});

				api.view = view;
				application.setContentView(view);
			},

			createWidget: function (dashboardId, type) {

				models.createWidget(dashboardId, type).done(api.initEditor);
			},

			editWidget: function (id) {

				models.editWidget(id).done(api.initEditor);
			}
		};

		return {
			start: function (action, id, type) {

				switch (action) {
					case "create":
						api.createWidget(id, type);
						break;
					case "edit":
						api.editWidget(id);
						break;
				}
			}
		};
	});