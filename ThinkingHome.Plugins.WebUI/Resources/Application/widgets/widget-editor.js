define(['app', 'lib',
		'application/settings/widget-editor-model.js',
		'application/settings/widget-editor-view.js'
],
	function (application, lib, models, views) {

		var api = {

			saveWidget: function() {

				// data
				var data = this.model.toJSON();
				data.displayName = this.ui.displayName.val();

				// fields
				var fields = this.getData();
				data.json = lib.json2.stringify(fields);

				models.saveWidget(data).done(api.openDashboard.bind(this));
			},

			deleteWidget: function () {

				var id = this.model.get("id"),
					displayName = this.ui.displayName.val();

				if (lib.utils.confirm('Do you want to delete the widget "{0}"?', displayName)) {

					models.deleteWidget(id).done(api.openDashboard.bind(this));
				}
			},

			openDashboardList: function() {

				application.navigate('application/settings/dashboard-list');
			},

			openDashboard: function () {

				var id = this.model.get("dashboardId");
				application.navigate('application/settings/widget-list', id);
			},

			initEditor: function (data) {

				var view = new views.WidgetEditorView({
					model: data.info,
					collection: data.fields
				});

				view.on("save:widget", api.saveWidget);
				view.on("delete:widget", api.deleteWidget);
				view.on("open:dashboard", api.openDashboard);
				view.on("open:dashboard:list", api.openDashboardList);

				application.setContentView(view);
			},

			createWidget: function (panelId, type) {

				models.createWidget(panelId, type).done(api.initEditor);
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