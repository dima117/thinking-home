define([
	'app',
	'webapp/webui/tile-params-model',
	'webapp/webui/tile-params-view'],
	function (application) {

		application.module('WebUI.TileParameters', function (module, app, backbone, marionette, $, _) {

			var api = {

				reload: function () {

					var view = new module.TileParametersView();

					app.setContentView(view);

					//app.request('load:tiles:all').done(function (collection) {

					//	var view = new module.TileCollectionView({
					//		collection: collection
					//	});

					//	app.setContentView(view);
					//});
				}
			};

			module.start = function (id) {
				api.reload(id);
			};
		});

		return application.WebUI.TileParameters;
	});