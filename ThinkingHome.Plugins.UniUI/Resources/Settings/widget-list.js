define(['app', 'common',
		'webapp/uniui/settings/widget-list-model.js',
		'webapp/uniui/settings/widget-list-view.js'
],
	function (application, common, models, views) {

		var api = {

			editWidget: function(view) {

				var id = view.model.get("id");

				application.navigate('webapp/uniui/settings/widget-editor', "edit", id);
			},

			openDashboardList: function () {

				application.navigate('webapp/uniui/settings/dashboard-list');
			},

			loadWidgetList: function (id) {

				models.loadWidgetList(id)
					.done(function (data) {

						var view = new views.WidgetListView({
							model: data.info,
							collection: data.widgets
						});

						view.on("open:dashboard:list", api.openDashboardList);
						view.on("childview:widget:edit", api.editWidget);

						api.view = view;
						application.setContentView(view);
					});
			}
		};

		return {
			start: api.loadWidgetList
		};
	});