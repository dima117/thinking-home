define(['app'], function (application) {

	application.module('Scripts.List', function (module, app, backbone, marionette, $, _) {

		// entities
		module.Script = backbone.Model.extend();

		module.ScriptCollection = backbone.Collection.extend({
			model: module.Script
		});

		// api
		var api = {

			loadScripts: function () {

				var defer = $.Deferred();

				$.getJSON('/api/scripts/list')
					.done(function (items) {
						var collection = new module.ScriptCollection(items);
						defer.resolve(collection);
					})
					.fail(function () {

						defer.resolve(undefined);
					});

				return defer.promise();
			}
		};

		// requests
		app.reqres.setHandler('load:scripts:list', function () {
			return api.loadScripts();
		});

	});

	return application.Scripts.List;
});