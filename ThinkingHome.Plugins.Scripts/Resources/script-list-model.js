define(['lib'], function (lib) {

	// entities
	var scriptListItem = lib.backbone.Model.extend();

	var scriptCollection = lib.backbone.Collection.extend({
		model: scriptListItem
	});

	// api
	var api = {

		loadScriptList: function () {

			var defer = lib.$.Deferred();

			lib.$.getJSON('/api/scripts/list')
				.done(function (items) {
					var collection = new scriptCollection(items);
					defer.resolve(collection);
				})
				.fail(function () {

					defer.resolve(undefined);
				});

			return defer.promise();
		},

		runScript: function (scriptId) {
			return lib.$.post('/api/scripts/run', { scriptId: scriptId }).promise();
		},

		deleteScript: function (scriptId) {
			return lib.$.post('/api/scripts/delete', { scriptId: scriptId }).promise();
		}
	};

	return {

		// entities
		ScriptListItem: scriptListItem,
		ScriptCollection: scriptCollection,

		// requests
		loadScriptList: api.loadScriptList,
		deleteScript: api.deleteScript,
		runScript: api.runScript
	};
});