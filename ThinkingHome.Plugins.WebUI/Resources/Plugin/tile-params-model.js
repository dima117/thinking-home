define(['app'], function (application) {

	application.module('WebUI.TileParameters', function (module, app, backbone, marionette, $, _) {

		// entities
		module.TileParamsForm = backbone.Model.extend({

		});

		// api
		var api = {

			load: function (id) {

				var defer = $.Deferred();

				$.getJSON('/api/webui/tiles/params', { id: id })
					.done(function (obj) {
						var model = new module.TileParamsForm(obj);
						defer.resolve(model);
					})
					.fail(function () {

						defer.resolve(undefined);
					});

				return defer.promise();
			}
		};

		// requests
		app.reqres.setHandler('load:tiles:params', api.load);
	});

	return application.WebUI.TileParameters;
});