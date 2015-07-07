define(['app',
		'application/dashboard-model.js',
		'application/dashboard-view.js'
],
	function (application, models, views) {

		var api = {

			load: function (id) {

				models.loadDetails(id).done(api.displayDetails);
			},

			select: function (item) {

				var id = item.model.get('id');
				application.navigate('dashboard', id);
			},

			displayDetails: function (details) {

				if (details) {

					var layout = new views.LayoutView();
					application.setContentView(layout);

					var menu = new views.MenuView({
						collection: details.dashboards
					});

					menu.on('childview:dashboard:select', api.select);

					layout.getRegion('menu').show(menu);
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