define(['app'], function (application) {

	application.module('Weather.Forecast', function (module, app, backbone, marionette, $, _) {

		module.WeatherData = backbone.Model.extend({
			
		});
		
		module.WeatherDataCollection = backbone.Collection.extend({
			model: module.WeatherData
		});

		module.WeatherLocation = backbone.Model.extend({
			
			initialize: function () {
				
				var now = this.get('now');
				if (now) {
					this.set('now', new module.WeatherData(now));
				}

				var day = this.get('day');
				if (day) {
					this.set('day', new module.WeatherDataCollection(day));
				}
				
				var forecast = this.get('forecast');
				if (forecast) {
					this.set('forecast', new module.WeatherDataCollection(forecast));
				}
			},

			toJSON: function () {
				
			var json = Backbone.Model.prototype.toJSON.apply(this, arguments);

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
		
		module.WeatherLocationCollection = backbone.Collection.extend({
			model: module.WeatherLocation
		});

		var api = {
			loadList: function () {

				var defer = $.Deferred();

				$.getJSON('/api/weather/all')
					.done(function (locations) {
						var collection = new module.WeatherLocationCollection(locations);
						defer.resolve(collection);
					})
					.fail(function () {

						defer.resolve(undefined);
					});

				return defer.promise();
			},
		};

		// requests
		app.reqres.setHandler('load:weather:forecast', api.loadList);
	});

	return application.Weather.Forecast;
});