define(
	['app', 'tpl!webapp/alarm-clock/editor.tpl'],
	function (application, editorTemplate) {

		application.module('AlarmClock.Editor', function (module, app, backbone, marionette, $, _) {

			module.AlarmEditorView = app.Common.FormView.extend({
				template: editorTemplate,
				events: {
					'click .js-btn-save': 'btnSaveClick',
					'click .js-btn-cancel': 'btnCancelClick'
				},
				btnSaveClick: function (e) {
					e.preventDefault();

					this.updateModel();
					this.trigger('alarm-clock:editor:save');
				},
				btnCancelClick: function (e) {
					e.preventDefault();
					this.trigger('alarm-clock:editor:cancel');
				}
			});
		});

		return application.AlarmClock.Editor;
	});