define(['lib'], function (lib) {

	// entities
	var scriptData = lib.backbone.Model.extend({
		defaults: {
			id: null,
			body: null
		}
	});

	// api
	var api = {

		loadScript: function (scriptId) {

			var defer = lib.$.Deferred();

			lib.$.getJSON('/api/scripts/get', { id: scriptId })
				.done(function (script) {
					var model = new scriptData(script);
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

			var rq = lib.$.post('/api/scripts/save', {
				id: scriptId,
				name: scriptName,
				body: scriptBody
			});

			return rq.promise();
		}
	};

	return {

		// entities
		ScriptData: scriptData,

		// requests
		loadScript: api.loadScript,
		saveScript: api.saveScript
	}
});