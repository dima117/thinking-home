define([
	'app',
	'webapp/webui/tile-params-model',
	'webapp/webui/tile-params-view'],
	function (application) {

		application.module('WebUI.TileParameters', function (module, app, backbone, marionette, $, _) {

			var api = {

				reload: function (id) {

					app.request('load:tiles:params', id).done(function (model) {

						var view = new module.TileParametersView({
							model: model
						});

						app.setContentView(view);
					});
				}
			};

			module.start = function (id) {
				api.reload(id);
			};
		});

		return application.WebUI.TileParameters;
	});