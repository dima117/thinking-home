define(
	['app',
		'webapp/weather/forecast-model',
		'webapp/weather/forecast-view'],
	function (application) {

		application.module('Weather.Forecast', function (module, app, backbone, marionette, $, _) {

			module.start = function () {
				
			};

		});

		return application.Weather.Forecast;
	});