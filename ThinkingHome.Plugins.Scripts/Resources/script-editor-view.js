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
					matchBrackets: true,
					extraKeys: {
						"Esc": function (cm) {
							if (cm.getOption("fullScreen")) cm.setOption("fullScreen", false);
						}
					}
				});
			},
			events: {
				'click .js-btn-save': 'btnSaveClick',
				'click .js-btn-cancel': 'btnCancelClick',
				'click .js-full-screen-editing': 'btnFullscreenEditing'
			},
			btnFullscreenEditing: function(e) {

				e.stopPropagation();
				e.preventDefault();

				this.cm.setOption("fullScreen", true);
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