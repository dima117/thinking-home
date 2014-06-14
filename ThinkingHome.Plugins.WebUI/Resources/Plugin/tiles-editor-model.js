define(['app'], function (application) {

	application.module('WebUI.TilesEditor', function (module, app, backbone, marionette, $, _) {

		module.TilesEditor = backbone.Model.extend();

		var api = {
			loadForm: function () {

				var defer = $.Deferred();

				$.getJSON('/api/webui/tiles/editor')
					.done(function (obj) {

						var model = new module.TilesEditor(obj);						
						defer.resolve(model);
					})
					.fail(function () {

						defer.resolve(undefined);
					});

				return defer.promise();
			}
		};
		
		// requests
		app.reqres.setHandler('load:tiles:editor-form', function () {
			return api.loadForm();
		});
	});

	return application.WebUI.TilesEditor;
});