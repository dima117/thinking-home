define(['lib',
		'application/dashboard-model.js',
		'application/dashboard-view.js',
		'json!api/webui/widgets.json'
],
	function (lib, models, views, widgetTypes) {

		var dashboard = lib.common.AppSection.extend({
			widgets: [],
			start: function (id) {
				models.loadDetails(id).done(this.bind('displayDetails'));
			},
			displayDetails: function (details) {

				if (details) {

					// layout
					var layout = new views.LayoutView({ model: details.panels });
					this.application.setContentView(layout, details.menuItems);

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