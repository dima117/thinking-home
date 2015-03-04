define(['lib'], function (lib) {

	// entities
	var location = lib.backbone.Model.extend();

	var locationCollection = lib.backbone.Collection.extend({
		model: location
	});

	// api
	var api = {

		loadLocations: function () {

			var defer = lib.$.Deferred();

			lib.$.getJSON('/api/weather/locations/list')
				.done(function (locations) {
					var collection = new locationCollection(locations);
					defer.resolve(collection);
				})
				.fail(function () {
					defer.resolve(undefined);
				});

			return defer.promise();
		},

		addLocation: function (displayName, query) {

			var rq = lib.$.post('/api/weather/locations/add', {
				displayName: displayName,
				query: query
			});

			return rq.promise();
		},

		deleteLocation: function (locationId) {

			return lib.$.post('/api/weather/locations/delete', { locationId: locationId }).promise();
		},

		updateLocation: function (locationId) {

			return lib.$.post('/api/weather/locations/update', { locationId: locationId }).promise();
		}
	};

	return {

		// entities
		Location: location,
		LocationCollection: locationCollection,

		// requests
		loadLocations: api.loadLocations,
		addLocation: api.addLocation,
		deleteLocation: api.deleteLocation,
		updateLocation: api.updateLocation
	};
});