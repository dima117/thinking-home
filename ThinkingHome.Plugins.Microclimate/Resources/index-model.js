define(
	['app', 'marionette', 'backbone', 'underscore'],
	function (application, marionette, backbone, _) {

		var api = {
			loadSensors: function() {

				var defer = $.Deferred();

				$.getJSON('/api/microclimate/sensors/list')
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
		application.reqres.setHandler('query:microclimate:sensors', api.loadSensors);

		return api;
	});