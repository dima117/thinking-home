define(['app',
		'application/dashboard-model.js',
		'application/dashboard-view.js'
],
	function (application, models, views) {

		var api = {

			load: function (id) {

				models.loadDetails(id).done(api.displayDetails);
			},

			displayDetails: function (details) {
				
				if (details) {

					var layout = new views.LayoutView();
					application.setContentView(layout);

				} else {

					var empty = new views.EmptyView();
					application.setContentView(empty);
				}
			}
		};

		return {
			start: api.load
		};
	});