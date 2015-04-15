define(
	['app', 'marionette', 'backbone', 'underscore'],
	function (application, marionette, backbone, _) {

		var api = {
			addSensor: function (displayName, channel, showHumidity) {

				var rq = $.post('/api/microclimate/sensors/add', {
					displayName: displayName,
					channel: channel,
					showHumidity: showHumidity
				});

				return rq.promise();
			},

			deleteSensor: function(id) {

				var rq = $.post('/api/microclimate/sensors/delete', { id: id });
				return rq.promise();
			},

			loadSensorTable: function() {

				var defer = $.Deferred();

				$.getJSON('/api/microclimate/sensors/table')
					.done(function (items) {

						var collection = new backbone.Collection(items);
						defer.resolve(collection);
					})
					.fail(function() {

						defer.resolve(undefined);
					});

				return defer.promise();
			}
		};

		// requests
		application.reqres.setHandler('query:microclimate:sensor:table', api.loadSensorTable);
		application.reqres.setHandler('cmd:microclimate:sensor:add', api.addSensor);
		application.reqres.setHandler('cmd:microclimate:sensor:delete', api.deleteSensor);

		return api;
	});