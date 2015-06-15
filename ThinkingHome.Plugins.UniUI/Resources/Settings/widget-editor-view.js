define([
	'lib',
	'text!webapp/uniui/settings/widget-editor.tpl',
	'text!webapp/uniui/settings/widget-editor-field.tpl'],
	function (lib, editorTemplate, fieldTemplate) {

		var fieldView = lib.marionette.ItemView.extend({
			template: lib._.template(fieldTemplate),
			ui: {
				field: ".js-field"
			},
			onRender: function () {

				var items = this.model.get("items");

				// build select list
				if (items && items.length) {

					for (var i = 0; i < items.length; i++) {

						lib.$("<option></option>")
							.text(items[i].displayName)
							.attr("value", items[i].id)
							.appendTo(this.ui.field);
					}
				}

				// set value
				var value = this.model.get("value");
				this.ui.field.val(value);
			}
		});
		var editorView = lib.marionette.CompositeView.extend({
			template: lib._.template(editorTemplate),
			childView: fieldView,
			childViewContainer: ".js-fields",
			ui: {
				displayName: ".js-display-name"
			},
			triggers: {
				"click .js-dashboard-list": "open:dashboard:list",
				"click .js-dashboard": "open:dashboard"
			},
			onRender: function () {

				var displayName = this.model.get("displayName");
				this.ui.displayName.val(displayName);
			}
		});

		return {
			WidgetEditorView: editorView
		};
	});