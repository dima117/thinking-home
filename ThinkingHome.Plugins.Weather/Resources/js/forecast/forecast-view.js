define([
		'app',
		'common',
		'text!webapp/weather/forecast.tpl',
		'text!webapp/weather/forecast-item.tpl',
		'text!webapp/weather/forecast-item-value.tpl',
		'text!webapp/weather/forecast-item-value-now.tpl'
], function (application, commonModule, template, itemTemplate, dataItemTemplate, nowDataItemTemplate) {

	application.module('Weather.Forecast', function (module, app, backbone, marionette, $, _) {

		// погода на текущий момент
		module.WeatherNowDataItemView = marionette.ItemView.extend({
			template: _.template(nowDataItemTemplate),
			onRender: function () {

				var cssClass = this.model.get('icon');
				var description = this.model.get('description');
				this.$('.js-weather-icon').addClass(cssClass).attr('title', description);
			}
		});

		// прогноз погоды
		module.WeatherDataItemView = module.WeatherNowDataItemView.extend({
			template: _.template(dataItemTemplate),
			tagName: 'li'
		});

		module.WeatherDataCollectionView = marionette.CollectionView.extend({
			childView: module.WeatherDataItemView,
			tagName: 'ul',
			className: 'list-unstyled weather-list'
		});


		module.WeatherForecastItemView = commonModule.ComplexView.extend({
			template: _.template(itemTemplate),
			parts: {
				now: {
					view: module.WeatherNowDataItemView,
					container: '.js-weather-now'
				},
				day: {
					view: module.WeatherDataCollectionView,
					container: '.js-weather-day'
				},
				forecast: {
					view: module.WeatherDataCollectionView,
					container: '.js-weather-forecast'
				}
			},
			triggers: {
				'click .js-btn-add-tile': 'weather:add-tile'
			}
		});

		module.WeatherForecastView = marionette.CompositeView.extend({
			template: _.template(template),
			childView: module.WeatherForecastItemView,
			childViewContainer: '.js-weather-list'
		});

	});

	return application.Weather.Forecast;
});