define(
	['lib', 'text!webapp/alarm-clock/editor.tpl'],
	function (lib, editorTemplate) {

		var alarmEditorView = lib.marionette.ItemView.extend({

			template: lib._.template(editorTemplate),
			ui: {
				scriptList: '.js-script-list'
			},
			onRender: function () {

				var data = this.serializeData();

				// add items
				lib.utils.addListItems(this.ui.scriptList, data.scripts);

				// set selected values
				lib.syphon.deserialize(this, data);

				// btn delete
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

				var data = lib.syphon.serialize(this);
				this.model.set(data);

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