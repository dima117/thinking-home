define([
		'lib',
		'text!webapp/weather/forecast.tpl',
		'text!webapp/weather/forecast-item.tpl',
		'text!webapp/weather/forecast-item-value.tpl',
		'text!webapp/weather/forecast-item-value-now.tpl',
		'lang!webapp/weather/lang.json'
], function (lib, template, itemTemplate, dataItemTemplate, nowDataItemTemplate, lang) {

	// погода на текущий момент
	var weatherNowDataItemView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile(nowDataItemTemplate),
		onRender: function () {
			if (this.model) {
				var cssClass = this.model.get('icon');
				var description = this.model.get('description');
				this.$('.js-weather-icon').addClass(cssClass).attr('title', description);
			}
		}
	});

	// прогноз погоды
	var weatherDataItemView = weatherNowDataItemView.extend({
		template: lib.handlebars.compile(dataItemTemplate),
		tagName: 'li'
	});

	var weatherDataCollectionView = lib.marionette.CollectionView.extend({
		childView: weatherDataItemView
	});


	var weatherForecastItemView = lib.marionette.ItemView.extend({
		template: lib.handlebars.compile(itemTemplate),
		ui: {
			now: '.js-weather-now',
			day: '.js-weather-day',
			forecast: '.js-weather-forecast'
		},
		onRender: function() {
			
			// now
			var viewNow = new weatherNowDataItemView({
				model: this.model.get('now'),
				el: this.ui.now
			});

			viewNow.render();

			// day
			var viewDay = new weatherDataCollectionView({
				collection: this.model.get('day'),
				el: this.ui.day
			});

			viewDay.render();

			// forecast
			var viewForecast = new weatherDataCollectionView({
				collection: this.model.get('forecast'),
				el: this.ui.forecast
			});

			viewForecast.render();
		},
		templateHelpers: { lang: lang }
	});

	var weatherForecastView = lib.marionette.CompositeView.extend({
		template: lib.handlebars.compile(template),
		childView: weatherForecastItemView,
		childViewContainer: '.js-weather-list',
		templateHelpers: { lang: lang }
	});

	return {
		WeatherNowDataItemView: weatherNowDataItemView,
		WeatherDataItemView: weatherDataItemView,
		WeatherDataCollectionView: weatherDataCollectionView,
		WeatherForecastItemView: weatherForecastItemView,
		WeatherForecastView: weatherForecastView
	};
});