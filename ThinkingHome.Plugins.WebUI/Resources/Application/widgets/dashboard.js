define(['app', 'common',
		'application/dashboard-model.js',
		'application/dashboard-view.js'
],
	function (application, common, models, views) {

		var api = {

			load: function () {

				var view = new views.DashboardView();

				application.setContentView(view);

				//models.loadDashboard()
				//	.done(function () {


				//	});
			}
		};

		return {
			start: api.load
		};
	});