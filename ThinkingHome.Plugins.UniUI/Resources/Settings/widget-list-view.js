define([
	'lib',
	'text!webapp/uniui/settings/widget-list.tpl',
	'text!webapp/uniui/settings/widget-list-item.tpl'],
	function (lib, listTemplate, widgetTemplate) {

		var widgetView = lib.marionette.ItemView.extend({
			template: lib._.template(widgetTemplate)
		});

		var listView = lib.marionette.CompositeView.extend({
			template: lib._.template(listTemplate),
			childView: widgetView,
			childViewContainer: '.js-list',
			triggers: {
				"click .js-dashboard-list": "open:dashboard:list"
			}
		});

		return {
			WidgetListView: listView
		};
	});