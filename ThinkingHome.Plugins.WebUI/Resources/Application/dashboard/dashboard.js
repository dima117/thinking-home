define(['app',
		'application/dashboard-model.js',
		'application/dashboard-view.js',
		'json!api/webui/widgets.json'
],
	function (application, models, views, widgetTypes) {

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

					// layout
					var layout = new views.LayoutView({ model: details.widgets });
					application.setContentView(layout);

					// menu
					var menu = new views.MenuView({
						collection: details.dashboards
					});

					menu.on('childview:dashboard:select', api.select);

					layout.getRegion('menu').show(menu);

					// widgets
					details.widgets.each(function (widget) {

						var type = widget.get("type"),
							widgetId = widget.get("id");
						

						var path = widgetTypes[type];

						if (path) {

							require([path], function (widgetModule) {

								var region = layout.addRegion(widgetId, "#" + widgetId);
								widgetModule.show(widget, region);
							});
						}
					});

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