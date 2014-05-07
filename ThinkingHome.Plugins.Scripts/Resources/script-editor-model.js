define(['app'], function (application) {

	application.module('Scripts.Editor', function (module, app, backbone, marionette, $, _) {

		// entities
		module.ScriptData = backbone.Model.extend();

		// api
		var api = {

			loadScript: function (scriptId) {

				var defer = $.Deferred();

				$.getJSON('/api/scripts/get', { id: scriptId })
					.done(function (script) {
						var model = new module.ScriptData(script);
						defer.resolve(model);
					})
					.fail(function () {
						defer.resolve(undefined);
					});

				return defer.promise();
			}
		};

		// requests
		app.reqres.setHandler('load:scripts:editor:load', function (scriptId) {
			return api.loadScript(scriptId);
		});

	});

	return application.Scripts.Editor;
});