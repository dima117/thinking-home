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
			ui: {
				editorPanel: '.js-editor-panel',
				btnFullscreen: '.js-full-screen',
				btnExitFullscreen: '.js-exit-full-screen'
			},
			events: {
				'click .js-btn-save': 'btnSaveClick',
				'click .js-btn-cancel': 'btnCancelClick',
				'click @ui.btnFullscreen': 'btnFullscreenEditing',
				'click @ui.btnExitFullscreen': 'btnExitFullscreenEditing'
			},
			btnFullscreenEditing: function(e) {

				e.stopPropagation();
				e.preventDefault();

				this.cm.setOption("fullScreen", true);
				this.ui.editorPanel.addClass("CodeMirror-panel-fullscreen");
				this.ui.btnFullscreen.addClass("hidden");
				this.ui.btnExitFullscreen.removeClass("hidden");
			},
			btnExitFullscreenEditing: function (e) {

				e.stopPropagation();
				e.preventDefault();

				this.cm.setOption("fullScreen", false);
				this.ui.editorPanel.removeClass("CodeMirror-panel-fullscreen");
				this.ui.btnFullscreen.removeClass("hidden");
				this.ui.btnExitFullscreen.addClass("hidden");
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