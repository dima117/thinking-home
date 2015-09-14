define([
	'lib',
	'text!application/settings/panel-list.tpl',
	'text!application/settings/widget-list-item.tpl',
	'text!application/settings/panel-list-item.tpl'],
	function (lib, listTemplate, widgetTemplate, panelTemplate) {

		var panelView = lib.marionette.ItemView.extend({
			template: lib.handlebars.compile(panelTemplate),
			triggers: {
				"click .js-panel-rename": "panel:rename",
				"click .js-panel-delete": "panel:delete"
		}
		});

		var listView = lib.marionette.CompositeView.extend({
			template: lib.handlebars.compile(listTemplate),
			childView: panelView,
			childViewContainer: '.js-list',
			triggers: {
				"click .js-dashboard-list": "open:dashboard:list",
				"click .js-create-panel": "panel:create"
			}
			//ui: {
			//	typeSelector: ".js-widget-type"
			//},
			//onRender: function () {

			//	// add items
			//	var types = this.model.get("types");
			//	lib.utils.addListItems(this.ui.typeSelector, types);
			//}
		});


		//var widgetView = lib.marionette.ItemView.extend({
		//	template: lib.handlebars.compile(widgetTemplate),
		//	triggers: {
		//		"click .js-edit-widget": "widget:edit"
		//	}
		//});

		return {
			PanelListView: listView
		};
	});