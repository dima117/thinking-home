define(
	['app', 'text!webapp/webui/tile-params.tpl'],
	function (application, template) {

		application.module('WebUI.TileParameters', function (module, app, backbone, marionette, $, _) {

			module.TileParametersView = marionette.ItemView.extend({
				template: _.template(template)
			});
		});

		return application.WebUI.TileParameters;
	});