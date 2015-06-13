define(['app', 'common',
		'webapp/uniui/settings/widget-list-model.js',
		'webapp/uniui/settings/widget-list-view.js'
],
	function (application, common, models, views) {

		var api = {

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

						api.view = view;
						application.setContentView(view);
					});
			}
		};

		return {
			start: api.loadWidgetList
		};
	});