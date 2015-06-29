define([
		'lib',
		'common',
		'text!webapp/weather/forecast.tpl',
		'text!webapp/weather/forecast-item.tpl',
		'text!webapp/weather/forecast-item-value.tpl',
		'text!webapp/weather/forecast-item-value-now.tpl'
], function (lib, common, template, itemTemplate, dataItemTemplate, nowDataItemTemplate) {

	// погода на текущий момент
	var weatherNowDataItemView = lib.marionette.ItemView.extend({
		template: lib._.template(nowDataItemTemplate),
		onRender: function () {

			var cssClass = this.model.get('icon');
			var description = this.model.get('description');
			this.$('.js-weather-icon').addClass(cssClass).attr('title', description);
		}
	});

	// прогноз погоды
	var weatherDataItemView = weatherNowDataItemView.extend({
		template: lib._.template(dataItemTemplate),
		tagName: 'li'
	});

	var weatherDataCollectionView = lib.marionette.CollectionView.extend({
		childView: weatherDataItemView,
		tagName: 'ul',
		className: 'list-unstyled weather-list'
	});


	var weatherForecastItemView = common.ComplexView.extend({
		template: lib._.template(itemTemplate),
		parts: {
			now: {
				view: weatherNowDataItemView,
				container: '.js-weather-now'
			},
			day: {
				view: weatherDataCollectionView,
				container: '.js-weather-day'
			},
			forecast: {
				view: weatherDataCollectionView,
				container: '.js-weather-forecast'
			}
		}
	});

	var weatherForecastView = lib.marionette.CompositeView.extend({
		template: lib._.template(template),
		childView: weatherForecastItemView,
		childViewContainer: '.js-weather-list'
	});

	return {
		WeatherNowDataItemView: weatherNowDataItemView,
		WeatherDataItemView: weatherDataItemView,
		WeatherDataCollectionView: weatherDataCollectionView,
		WeatherForecastItemView: weatherForecastItemView,
		WeatherForecastView: weatherForecastView
	};
});