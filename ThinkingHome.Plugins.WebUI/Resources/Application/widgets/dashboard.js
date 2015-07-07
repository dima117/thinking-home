define(['app',
		'application/dashboard-model.js',
		'application/dashboard-view.js'
],
	function (application, models, views) {

		var api = {

			load: function (id) {

				var empty = new views.EmptyView();
				application.setContentView(empty);

				var layout = new views.LayoutView();
				application.setContentView(layout);

				//models.loadDashboardList()
				//	.done(function (list) {

				//		var nav = new views.NavPanelView({
				//			collection: list
				//		});

				//		nav.on("childview:nav:select", function (view) {

				//			var electedId = view.model.get("id");
				//			api.setSelectedItem(electedId, list);
				//		});

				//		layout.getRegion('nav').show(nav);

				//		api.setSelectedItem(id, list);
				//	});
			}
		};

		return {
			start: api.load
		};
	});