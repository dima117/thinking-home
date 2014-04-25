define(['app', 'tpl!webapp/webui/settings.tpl'], function (application, template) {

	application.module('WebUI.Settings', function(module, app, backbone, marionette, $, _) {

		module.createView = function () {
			
			var view = new marionette.ItemView({
				template: template
			});

			return view;
		};
	});

	return application.WebUI.Settings;
});