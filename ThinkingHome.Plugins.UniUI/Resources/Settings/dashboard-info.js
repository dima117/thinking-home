define(['app', 'common',
		'webapp/uniui/settings/dashboard-info-model.js',
		'webapp/uniui/settings/dashboard-info-view.js'
],
	function (application, common, models, views) {

		var api = {

			loadDashboardInfo: function(id) {

				models.loadDashboardInfo(id)
					.done(function(data) {

						var view = new views.DashboardInfoView({
							model: data.info,
							collection: data.widgets
						});

						api.view = view;
						application.setContentView(view);
					});
			}
		};

		return {
			start: api.loadDashboardInfo
		};
	});