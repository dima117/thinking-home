define(['app',
		'application/dashboard-model.js',
		'application/dashboard-view.js'
],
	function (application, models, views) {

		var api = {

			setSelectedItem: function (id, list) {

				var el = list.get(id) || list.first();

				if (el) {

					el.set('active', true);

					models.loadDashboardDetails(el.id)
						.done(function (widgets) {

							alert(1);
						});
				}
			},

			load: function (id) {

				var layout = new views.LayoutView();
				application.setContentView(layout);

				models.loadDashboardList()
					.done(function (list) {

						var nav = new views.NavPanelView({
							collection: list
						});

						layout.getRegion('nav').show(nav);

						api.setSelectedItem(id, list);
					});
			}
		};

		return {
			start: api.load
		};
	});