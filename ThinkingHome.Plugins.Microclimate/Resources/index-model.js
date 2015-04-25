define(
	['lib'],
	function (lib) {

		var api = {
			loadSensors: function() {

				var defer = lib.$.Deferred();

				lib.$.getJSON('/api/microclimate/sensors/list')
					.done(function (items) {

						var collection = new lib.backbone.Collection(items);
						defer.resolve(collection);
					})
					.fail(function() {

						defer.resolve(undefined);
					});

				return defer.promise();
			}
		};

		return {
			loadSensors: api.loadSensors
		};
	});