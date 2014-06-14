define(['app', 'webapp/webui/tiles-editor-model', 'webapp/webui/tiles-editor-view'], function (application) {

	application.module('WebUI.TilesEditor', function (module, app, backbone, marionette, $, _) {

		var layoutView;

		var api = {

			reloadForm: function () {

				app.request('load:tiles:editor-form')
					.done(function (formData) {

						var form = new module.TilesEditorFormView({ model: formData });
						//form.on('scripts:subscription:add', api.addSubscription);
						layoutView.regionForm.show(form);
					});
			}
		};

		module.start = function () {

			// init layout
			layoutView = new module.TilesEditorLayout();
			app.setContentView(layoutView);

			api.reloadForm();
			//api.reloadList();
		};
	});

	return application.WebUI.TilesEditor;
});