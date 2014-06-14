define(
	[	'app',
		'tpl!webapp/webui/tiles-editor-layout.tpl',
		'tpl!webapp/webui/tiles-editor-form.tpl'],
	function (application, layoutTemplate, formTemplate) {

	application.module('WebUI.TilesEditor', function (module, app, backbone, marionette, $, _) {

		module.TilesEditorLayout = marionette.Layout.extend({
			template: layoutTemplate,
			regions: {
				regionForm: '#region-form',
				regionList: '#region-list'
			}
		});

		module.TilesEditorFormView = app.Common.FormView.extend({
			template: formTemplate
		});
	});

	return application.WebUI.TilesEditor;
});