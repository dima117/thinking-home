define([
		'app',
		'common',
		'text!webapp/weather/locations-layout.tpl',
		'text!webapp/weather/locations-form.tpl',
		'text!webapp/weather/locations-list.tpl',
		'text!webapp/weather/locations-list-item.tpl'
], function (application, commonModule, layoutTemplate, formTemplate, listTemplate, itemTemplate) {

	application.module('Weather.Settings', function (module, app, backbone, marionette, $, _) {

		module.WeatherSettingsLayout = marionette.Layout.extend({
			template: _.template(layoutTemplate),
			regions: {
				regionForm: '#region-weather-locations-form',
				regionList: '#region-weather-locations-list'
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
		
		module.LocationView = marionette.ItemView.extend({
			template: _.template(itemTemplate),
			tagName: 'tr',
			triggers: {
				'click .js-update-location': 'weather:location:update',
				'click .js-delete-location': 'weather:location:delete'
			},
			showSpinner: function() {
				this.$('.js-update-location-spin').removeClass('hidden');
			},
			hideSpinner: function () {
				this.$('.js-update-location-spin').addClass('hidden');
			}
		});

		module.LocationListView = marionette.CompositeView.extend({
			template: _.template(listTemplate),
			itemView: module.LocationView,
			itemViewContainer: 'tbody'
		});
		
	});

	return application.Weather.Settings;
});