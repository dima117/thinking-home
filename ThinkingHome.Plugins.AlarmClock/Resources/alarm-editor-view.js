define(
	['lib', 'common', 'text!webapp/alarm-clock/editor.tpl'],
	function (lib, common, editorTemplate) {

		var alarmEditorView = common.FormView.extend({

			template: lib._.template(editorTemplate),
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

		return {
			AlarmEditorView: alarmEditorView
		};
	});