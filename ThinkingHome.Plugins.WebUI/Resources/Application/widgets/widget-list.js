define(['app',
		'application/settings/widget-list-model.js',
		'application/settings/widget-list-view.js'
],
	function (application, models, views) {

		var api = {

			createWidget: function() {

				var id = this.model.get("id"),
					type = this.ui.typeSelector.val();

				application.navigate('application/settings/widget-editor', "create", id, type);
			},

			editWidget: function(view) {

				var id = view.model.get("id");

				application.navigate('application/settings/widget-editor', "edit", id);
			},

			openDashboardList: function () {

				application.navigate('application/settings/dashboard-list');
			},

			loadWidgetList: function (id) {

				models.loadWidgetList(id)
					.done(function (data) {

						var view = new views.WidgetListView({
							model: data.info,
							collection: data.widgets
						});

						view.on("widget:create", api.createWidget);
						view.on("childview:widget:edit", api.editWidget);
						view.on("open:dashboard:list", api.openDashboardList);

						application.setContentView(view);
					});
			}
		};

		return {
			start: api.loadWidgetList
		};
	});