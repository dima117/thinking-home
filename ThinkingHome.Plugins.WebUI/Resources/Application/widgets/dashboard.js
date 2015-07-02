define(['app',
		'application/dashboard-model.js',
		'application/dashboard-view.js'
],
	function (application, models, views) {

		var api = {

			load: function () {

				var layout = new views.LayoutView();
				application.setContentView(layout);

				models.loadDashboardList()
					.done(function (list) {

						var nav = new views.NavPanelView({
							collection: list
						});

						layout.getRegion('nav').show(nav);
					});
			}
		};

		return {
			start: api.load
		};
	});