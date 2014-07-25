define([
		'app',
		'common',
		'text!webapp/weather/settings-layout.tpl',
		'text!webapp/weather/settings-form.tpl'
], function (application, commonModule, layoutTemplate, formTemplate) {

	application.module('Weather.Settings', function (module, app, backbone, marionette, $, _) {

		module.WeatherSettingsLayout = marionette.Layout.extend({
			template: _.template(layoutTemplate),
			regions: {
				regionForm: '#region-weather-settings-form',
				regionList: '#region-weather-settings-list'
			}
		});
		
		module.WeatherSettingsFormView = commonModule.FormView.extend({
			template: _.template(formTemplate),
			events: {
				'click .js-btn-add-location': 'addLocation'
			},
			addLocation: function (e) {
				e.preventDefault();

				//this.updateModel();
				this.trigger('weather:location:add');
			}
		});
		
	});

	return application.Weather.Settings;
});