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
			}
		};
		
		// requests
		app.reqres.setHandler('load:weather:locations', api.loadLocations);
	});

	return application.Weather.Settings;
});