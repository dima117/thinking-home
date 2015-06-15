define([
	'lib',
	'text!webapp/uniui/settings/widget-list.tpl',
	'text!webapp/uniui/settings/widget-list-item.tpl'],
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
				"click .js-dashboard-list": "open:dashboard:list"
			},
			ui: {
				typeSelector: ".js-widget-type"
			},
			onRender: function() {
				
				var types = this.model.get("types");

				// build select list
				if (types && types.length) {

					for (var i = 0; i < types.length; i++) {

						lib.$("<option></option>")
							.text(types[i].displayName)
							.attr("value", types[i].id)
							.appendTo(this.ui.typeSelector);
					}
				}
			}
		});

		return {
			WidgetListView: listView
		};
	});