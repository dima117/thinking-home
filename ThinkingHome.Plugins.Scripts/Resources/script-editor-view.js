define(
	['app', 'tpl!webapp/scripts/script-editor.tpl', 'tpl!webapp/scripts/script-editor-layout.tpl'],
	function (application, editorTemplate, layoutTemplate) {

		application.module('Scripts.Editor', function (module, app, backbone, marionette, $, _) {

			module.ScriptEditorView = marionette.ItemView.extend({
				template: editorTemplate,
				events: {
					'click .js-btn-save': 'btnSaveClick',
					'click .js-btn-cancel': 'btnCancelClick'
				},
				btnSaveClick: function (e) {
					e.preventDefault();
					this.trigger('scripts:editor:save');
				},
				btnCancelClick: function (e) {
					e.preventDefault();
					this.trigger('scripts:editor:cancel');
				}
			});
			
			module.ScriptEditorLayout = marionette.Layout.extend({
				template: layoutTemplate,
				regions: {
					regionContent: '.ph-content'
				}
			});
		});

		return application.Scripts.Editor;
	});