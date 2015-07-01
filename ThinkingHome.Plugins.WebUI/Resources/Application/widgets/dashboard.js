define(['app',
		'application/dashboard-model.js',
		'application/dashboard-view.js'
],
	function (application, models, views) {

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