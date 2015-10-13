define(['lib',
		'application/dashboard-model.js',
		'application/dashboard-view.js',
		'json!api/webui/widgets.json'
],
	function (lib, models, views, widgetTypes) {

		// todo: реализовать onBeforeDestroy
		var dashboard = lib.common.AppSection.extend({
			start: function (id) {
				models.loadDetails(id)
					.done(this.bindFnContext('displayDetails'));
			},
			onSelect: function (item) {
				var id = item.model.get('id');
				this.application.navigate('dashboard', id);
			},
			displayDetails: function (details) {

				if (details) {

					// layout
					var layout = this.view = new views.LayoutView({ model: details.panels });
					this.application.setContentView(this.view);

					// menu
					var menu = new views.MenuView({
						collection: details.dashboards
					});

					menu.on('childview:dashboard:select', this.bindFnContext('onSelect'));
					this.view.getRegion('menu').show(menu);

					// widgets
					details.panels.each(function (panel) {

						panel.get('widgets').each(function (widget) {

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
					});
				} else {
					this.view = new views.EmptyView();
					this.application.setContentView(this.view);
				}
			}
		});

		return dashboard;
	});