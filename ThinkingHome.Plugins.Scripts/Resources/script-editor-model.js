define(['app'], function (application) {

	application.module('Scripts.Editor', function (module, app, backbone, marionette, $, _) {

		// entities
		module.ScriptData = backbone.Model.extend({
			defaults: {
				id: null,
				body: null
			}
		});

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
			},
			saveScript: function (model) {

				var scriptId = model.get('id');
				var scriptName = model.get('name');
				var scriptBody = model.get('body');

				var rq = $.post('/api/scripts/save', {
					id: scriptId,
					name: scriptName,
					body: scriptBody
				});

				return rq.promise();
			}
		};

		// requests
		app.reqres.setHandler('load:scripts:editor:load', api.loadScript);
		app.reqres.setHandler('update:scripts:editor:save', api.saveScript);
	});

	return application.Scripts.Editor;
});