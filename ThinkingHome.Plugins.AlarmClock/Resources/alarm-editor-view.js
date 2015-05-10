define(
	['lib', 'common', 'text!webapp/alarm-clock/editor.tpl'],
	function (lib, common, editorTemplate) {

		var alarmEditorView = common.FormView.extend({

			template: lib._.template(editorTemplate),
			onShow: function() {

				var hasId = !!this.model.get("id");
				this.$(".js-btn-delete").toggle(hasId);
			},
			events: {
				'click .js-btn-save': 'btnSaveClick',
				'click .js-btn-cancel': 'btnCancelClick',
				'click .js-btn-delete': 'btnDeleteClick'
			},
			btnSaveClick: function (e) {
				e.preventDefault();

				this.updateModel();
				this.trigger('alarm-clock:editor:save');
			},
			btnCancelClick: function (e) {
				e.preventDefault();
				this.trigger('alarm-clock:editor:cancel');
			},
			btnDeleteClick: function (e) {
				e.preventDefault();
				this.trigger('alarm-clock:editor:delete');
			}
		});

		return {
			AlarmEditorView: alarmEditorView
		};
	});