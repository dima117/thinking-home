define(['app', 'common',
		'webapp/uniui/settings/dashboard-list-model.js',
		'webapp/uniui/settings/dashboard-list-view.js'
],
	function (application, common, models, views) {

		var api = {

			loadDashboardList: function () {

				models.loadDashboardList()
					.done(function (list) {

						var view = new views.DashboardListView({
							collection: list
						});

						application.setContentView(view);
					});
			}
		};

		return {
			start: api.loadDashboardList
		};
	});