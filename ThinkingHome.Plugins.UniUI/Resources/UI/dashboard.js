define(['app', 'common',
		'webapp/uniui/ui/dashboard-model.js',
		'webapp/uniui/ui/dashboard-view.js'
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