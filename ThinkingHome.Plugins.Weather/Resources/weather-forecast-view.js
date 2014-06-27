define([
		'app',
		'text!webapp/weather/forecast.tpl',
		'text!webapp/weather/forecast-item.tpl'], function (application, template, itemTemplate) {

	application.module('Weather.Forecast', function (module, app, backbone, marionette, $, _) {

		module.WeatherForecastItemView = marionette.ItemView.extend({
			template: _.template(itemTemplate)
		});
		
		module.WeatherForecastView = marionette.CompositeView.extend({
			template: _.template(template),
			itemView: module.WeatherForecastItemView,
			itemViewContainer: '.js-list'
		});

	});

	return application.Weather.Forecast;
});