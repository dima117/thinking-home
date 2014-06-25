define(['app', 'text!webapp/weather/forecast.tpl'], function (application, template) {

	application.module('Weather.Forecast', function (module, app, backbone, marionette, $, _) {

		module.WeatherForecastView = marionette.ItemView.extend({
			template: _.template(template)
		});

	});

	return application.Weather.Forecast;
});