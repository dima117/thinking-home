define([
		'app',
		'common',
		'text!webapp/weather/settings-layout.tpl'
], function (application, commonModule, layoutTemplate) {

	application.module('Weather.Settings', function (module, app, backbone, marionette, $, _) {

		module.WeatherSettingsLayout = marionette.Layout.extend({
			template: _.template(layoutTemplate),
			regions: {
				regionForm: '#region-weather-settings-form',
				regionList: '#region-weather-settings-list'
			}
		});
	});

	return application.Weather.Settings;
});