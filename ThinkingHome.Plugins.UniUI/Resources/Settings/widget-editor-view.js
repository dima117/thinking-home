define([
	'lib',
	'text!webapp/uniui/settings/widget-editor.tpl'],
	function (lib, editorTemplate) {

		var editorView = lib.marionette.ItemView.extend({
			template: lib._.template(editorTemplate)
		});

		return {
			WidgetEditorView: editorView
		};
	});