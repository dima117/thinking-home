define([
	'lib',
	'common',
	'text!webapp/uniui/settings/widget-editor.tpl',
	'text!webapp/uniui/settings/widget-editor-field.tpl'],
	function (lib, common, editorTemplate, fieldTemplate) {

		var fieldView = lib.marionette.ItemView.extend({
			template: lib._.template(fieldTemplate),
			ui: {
				field: ".js-field"
			},
			onRender: function () {

				// add items
				var items = this.model.get("items");
				common.utils.addListItems(this.ui.field, items);

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
				displayName: ".js-display-name",
				btnDelete: ".js-delete"
			},
			triggers: {
				"click .js-dashboard-list": "open:dashboard:list",
				"click .js-dashboard": "open:dashboard",
				"click .js-delete": "delete:widget",
				"click .js-cancel": "open:dashboard",
				"click .js-save": "save:widget"
			},
			onRender: function () {

				// display name
				var displayName = this.model.get("displayName");
				this.ui.displayName.val(displayName);

				// btn delete
				var isNew = !this.model.get("id");
				this.ui.btnDelete.toggleClass("hidden", isNew);
			},
			getData: function() {

				var data = lib.syphon.serialize(this);
				return data;
			}
		});

		return {
			WidgetEditorView: editorView
		};
	});