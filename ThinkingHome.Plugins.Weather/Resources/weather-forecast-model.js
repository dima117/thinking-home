define(['app'], function (application) {

	application.module('Weather.Forecast', function (module, app, backbone, marionette, $, _) {

		module.WeatherLocation = backbone.Model.extend({});
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