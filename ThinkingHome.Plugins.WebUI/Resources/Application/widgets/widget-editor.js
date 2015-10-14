define(['lib',
		'application/settings/widget-editor-model.js',
		'application/settings/widget-editor-view.js'
	],
	function (lib, models, views) {

		var widgetEditor = lib.common.AppSection.extend({
			start: function (action, id, type) {

				switch (action) {
					case "create":
						this.createWidget(id, type);
						break;
					case "edit":
						this.editWidget(id);
						break;
				}
			},

			saveWidget: function (view) {

				// data
				var data = view.model.toJSON();
				data.displayName = view.ui.displayName.val();

				// fields
				var fields = view.getData();
				data.json = lib.json2.stringify(fields);

				models.saveWidget(data).done(this.bind('openDashboard', view));
			},

			deleteWidget: function (view) {
				var id = view.model.get("id"),
					displayName = view.ui.displayName.val();

				if (lib.utils.confirm('Do you want to delete the widget "{0}"?', displayName)) {
					models.deleteWidget(id).done(this.bind('openDashboard', view));
				}
			},

			openDashboardList: function () {
				this.application.navigate('application/settings/dashboard-list');
			},

			openDashboard: function (view) {
				var id = view.model.get("dashboardId");
				this.application.navigate('application/settings/widget-list', id);
			},

			initEditor: function (data) {
				var view = new views.WidgetEditorView({
					model: data.info,
					collection: data.fields
				});

				this.listenTo(view, 'save:widget', this.bind('saveWidget', view));
				this.listenTo(view, 'delete:widget', this.bind('deleteWidget', view));
				this.listenTo(view, 'open:dashboard', this.bind('openDashboard', view));
				this.listenTo(view, 'open:dashboard:list', this.bind('openDashboardList'));

				this.application.setContentView(view);
			},

			createWidget: function (panelId, type) {
				models.createWidget(panelId, type).done(this.bind('initEditor'));
			},

			editWidget: function (id) {
				models.editWidget(id).done(this.bind('initEditor'));
			}
		});

		return widgetEditor;
	});