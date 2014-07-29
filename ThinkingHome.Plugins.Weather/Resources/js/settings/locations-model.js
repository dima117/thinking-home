define(['app'], function (application) {

	application.module('Weather.Settings', function (module, app, backbone, marionette, $, _) {

		// entities
		module.FormData = backbone.Model.extend();

		module.Location = backbone.Model.extend();

		module.LocationCollection = backbone.Collection.extend({
			model: module.Location
		});
		
		// api
		var api = {
			
			loadLocations: function () {

				var defer = $.Deferred();

				$.getJSON('/api/weather/locations/list')
					.done(function (locations) {
						var collection = new module.LocationCollection(locations);
						defer.resolve(collection);
					})
					.fail(function () {
						defer.resolve(undefined);
					});

				return defer.promise();
			},
			
			addLocation: function (displayName, query) {

				var rq = $.post('/api/weather/locations/add', {
					displayName: displayName,
					query: query
				});

				return rq.promise();
			},
			
			deleteLocation: function (locationId) {

				var rq = $.post('/api/weather/locations/delete', {
					locationId: locationId
				});

				return rq.promise();
			},
			
			updateLocation: function (locationId) {

				var rq = $.post('/api/weather/locations/update', {
					locationId: locationId
				});

				return rq.promise();
			}
		};
		
		// requests
		app.reqres.setHandler('query:weather:locations', api.loadLocations);
		app.reqres.setHandler('cmd:weather:locations-add', api.addLocation);
		app.reqres.setHandler('cmd:weather:locations-delete', api.deleteLocation);
		app.reqres.setHandler('cmd:weather:locations-update', api.updateLocation);
	});

	return application.Weather.Settings;
});