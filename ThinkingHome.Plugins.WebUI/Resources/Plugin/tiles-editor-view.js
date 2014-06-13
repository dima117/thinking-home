define(['app', 'tpl!webapp/webui/tile.tpl'], function (application, template) {

	application.module('WebUI.TilesEditor', function (module, app, backbone, marionette, $, _) {

		module.TilesEditorLayout = marionette.Layout.extend({
			template: layoutTemplate,
			regions: {
				regionForm: '#region-subscriptions-form',
				regionList: '#region-subscriptions-list'
			}
		});

		module.TilesEditorView = app.Common.FormView.extend({
			template: editorTemplate,
			onShow: function () {

				var textarea = this.$('.js-script-body')[0];

				this.cm = codemirror.fromTextArea(textarea, {
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

				this.cm.save();
				var data = Backbone.Syphon.serialize(this);
				this.trigger('scripts:editor:save', data);
			},
			btnCancelClick: function (e) {
				e.preventDefault();
				this.trigger('scripts:editor:cancel');
			}
		});
	});

	return application.WebUI.TilesEditor;
});