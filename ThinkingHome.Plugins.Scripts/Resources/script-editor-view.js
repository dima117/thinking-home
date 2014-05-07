define(
	['app',
		'codemirror',
		'tpl!webapp/scripts/script-editor.tpl',
		'tpl!webapp/scripts/script-editor-layout.tpl'
	],
	function (application, codemirror, editorTemplate, layoutTemplate) {
	//
		application.module('Scripts.Editor', function (module, app, backbone, marionette, $, _) {

			module.ScriptEditorView = marionette.ItemView.extend({
				template: editorTemplate,
				onRender: function () {

					var textarea = this.$('.js-script-body')[0];
					codemirror.fromTextArea(textarea, {
						mode: 'javascript',
						theme: 'bootstrap',
						lineNumbers: true,
						styleActiveLine: true,
						matchBrackets: true
					});
				},
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