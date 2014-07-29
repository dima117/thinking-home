define(['app'], function (application) {

	application.module('Scripts.List', function (module, app, backbone, marionette, $, _) {

		// entities
		module.ScriptListItem = backbone.Model.extend();

		module.ScriptCollection = backbone.Collection.extend({
			model: module.ScriptListItem
		});

		// api
		var api = {

			loadScriptList: function () {

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
			},

			runScript: function (scriptId) {
				return $.post('/api/scripts/run', { scriptId: scriptId }).promise();
			},

			deleteScript: function (scriptId) {
				return $.post('/api/scripts/delete', { scriptId: scriptId }).promise();
			}
		};

		// requests
		app.reqres.setHandler('query:scripts:list', api.loadScriptList);
		app.reqres.setHandler('cmd:scripts:delete', api.deleteScript);
		app.reqres.setHandler('cmd:scripts:run', api.runScript);
	});

	return application.Scripts.List;
});