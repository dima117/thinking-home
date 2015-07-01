define([
	'lib',
	'text!application/settings/widget-list.tpl',
	'text!application/settings/widget-list-item.tpl'],
	function (lib, listTemplate, widgetTemplate) {

		var widgetView = lib.marionette.ItemView.extend({
			template: lib._.template(widgetTemplate),
			triggers: {
				"click .js-edit-widget": "widget:edit"
			}
		});

		var listView = lib.marionette.CompositeView.extend({
			template: lib._.template(listTemplate),
			childView: widgetView,
			childViewContainer: '.js-list',
			triggers: {
				"click .js-dashboard-list": "open:dashboard:list",
				"click .js-create-widget": "widget:create"
			},
			ui: {
				typeSelector: ".js-widget-type"
			},
			onRender: function() {

				// add items
				var types = this.model.get("types");
				lib.utils.addListItems(this.ui.typeSelector, types);
			}
		});

		return {
			WidgetListView: listView
		};
	});