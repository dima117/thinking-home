define(['app', 'tpl!webapp/weather/forecast.tpl'], function (application, template) {

	application.module('Weather.Forecast', function (module, app, backbone, marionette, $, _) {

		module.WeatherForecastView = marionette.ItemView.extend({
			template: template
		});

	});

	return application.Weather.Forecast;
});