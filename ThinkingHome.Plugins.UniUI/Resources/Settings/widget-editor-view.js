define([
	'lib',
	'text!webapp/uniui/settings/widget-editor.tpl',
	'text!webapp/uniui/settings/widget-editor-field.tpl'],
	function (lib, editorTemplate, fieldTemplate) {

		var fieldView = lib.marionette.ItemView.extend({
			template: lib._.template(fieldTemplate)
		});
		var editorView = lib.marionette.CompositeView.extend({
			template: lib._.template(editorTemplate),
			childView: fieldView,
			childViewContainer: ".js-fields"
		});

		return {
			WidgetEditorView: editorView
		};
	});