define(
	['lib'],
	function (lib) {

		var api = {
			addSensor: function (displayName, channel, showHumidity) {

				var rq = lib.$.post('/api/microclimate/sensors/add', {
					displayName: displayName,
					channel: channel,
					showHumidity: showHumidity
				});

				return rq.promise();
			},

			deleteSensor: function(id) {

				var rq = lib.$.post('/api/microclimate/sensors/delete', { id: id });
				return rq.promise();
			},

			loadSensorTable: function() {

				var defer = lib.$.Deferred();

				lib.$.getJSON('/api/microclimate/sensors/table')
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
			loadSensorTable: api.loadSensorTable,
			addSensor: api.addSensor,
			deleteSensor: api.deleteSensor
		};
	});