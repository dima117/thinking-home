define(['lib',
		'application/dashboard-model.js',
		'application/dashboard-view.js',
		'json!api/webui/widgets.json'
],
	function (lib, models, views, widgetTypes) {

		// todo: реализовать onBeforeDestroy
		var dashboard = lib.common.AppSection.extend({
			widgets: [],
			start: function (id) {
				models.loadDetails(id).done(this.bind('displayDetails'));
			},
			onSelect: function (item) {
				var id = item.model.get('id');
				this.application.navigate('dashboard', id);
			},
			displayDetails: function (details) {

				if (details) {

					// layout
					var layout = new views.LayoutView({ model: details.panels });
					this.application.setContentView(layout);

					// menu
					var menu = new views.MenuView({
						collection: details.dashboards
					});

					this.listenTo(menu, 'childview:dashboard:select', this.bind('onSelect'));
					layout.getRegion('menu').show(menu);

					// widgets
					details.panels.each(function (panel) {

						panel.get('widgets').each(function (widgetModel) {

							var type = widgetModel.get("type"),
								widgetId = widgetModel.get("id"),
								path = widgetTypes[type],
								self = this;

							path && require([path], function (widgetConstructor) {
								var region = layout.addRegion(widgetId, "#" + widgetId), 
									widget = new widgetConstructor({
										application: self.application,
										region: region
									});

								widget.show(widgetModel);
								self.widgets.push(widget);
							});
						}, this);
					}, this);
				} else {
					var empty = new views.EmptyView();
					this.application.setContentView(empty);
				}
			},
			onBeforeDestroy: function () {
				this.widgets.forEach(function (widget) {
					widget.destroy();
				});
			}
		});

		return dashboard;
	});