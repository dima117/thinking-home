define(
	['lib',
		'common',
		'codemirror',
		'text!webapp/scripts/script-editor.tpl'
	],
	function (lib, common, codemirror, editorTemplate) {

		var scriptEditorView = common.FormView.extend({
			template: lib._.template(editorTemplate),
			onShow: function () {

				var textarea = this.$('.js-script-body')[0];

				this.cm = codemirror.fromTextArea(textarea, {
					mode: 'javascript',
					theme: 'bootstrap-dark',
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

				this.cm.save();
				var data = lib.backbone.Syphon.serialize(this);
				this.trigger('scripts:editor:save', data);
			},
			btnCancelClick: function (e) {
				e.preventDefault();
				this.trigger('scripts:editor:cancel');
			}
		});

		return {
			ScriptEditorView: scriptEditorView
		};
	});