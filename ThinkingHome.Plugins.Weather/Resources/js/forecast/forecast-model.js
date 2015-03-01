define(['lib'], function (lib) {

	// entities
	var weatherData = lib.backbone.Model.extend();

	var weatherDataCollection = lib.backbone.Collection.extend({
		model: weatherData
	});

	var weatherLocation = lib.backbone.Model.extend({

		initialize: function () {

			var now = this.get('now');
			if (now) {
				this.set('now', new weatherData(now));
			}

			var day = this.get('day');
			if (day) {
				this.set('day', new weatherDataCollection(day));
			}

			var forecast = this.get('forecast');
			if (forecast) {
				this.set('forecast', new weatherDataCollection(forecast));
			}
		},

		toJSON: function () {

			var json = lib.backbone.Model.prototype.toJSON.apply(this, arguments);

			if (json.now) {
				json.now = json.now.toJSON();
			}

			if (json.day) {
				json.day = json.day.toJSON();
			}

			if (json.forecast) {
				json.forecast = json.forecast.toJSON();
			}

			return json;
		}
	});

	var weatherLocationCollection = lib.backbone.Collection.extend({
		model: weatherLocation
	});

	var api = {
		loadList: function () {

			var defer = lib.$.Deferred();

			lib.$.getJSON('/api/weather/all')
				.done(function (locations) {

					var collection = new weatherLocationCollection(locations);
					defer.resolve(collection);
				})
				.fail(function () {

					defer.resolve(undefined);
				});

			return defer.promise();
		},
	};

	return {

		// entities
		WeatherData: weatherData,
		WeatherDataCollection: weatherDataCollection,
		WeatherLocation: weatherLocation,
		WeatherLocationCollection: weatherLocationCollection,

		// requests
		loadList: api.loadList
	};
});