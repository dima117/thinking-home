define([
		'lib',
		'text!webapp/weather/locations-layout.tpl',
		'text!webapp/weather/locations-form.tpl',
		'text!webapp/weather/locations-list.tpl',
		'text!webapp/weather/locations-list-item.tpl'
], function (lib, layoutTemplate, formTemplate, listTemplate, itemTemplate) {

	var weatherSettingsLayout = lib.marionette.LayoutView.extend({
		template: lib._.template(layoutTemplate),
		regions: {
			regionForm: '#region-weather-locations-form',
			regionList: '#region-weather-locations-list'
		}
	});

	var weatherSettingsFormView = lib.marionette.ItemView.extend({
		template: lib._.template(formTemplate),
		events: {
			'click .js-btn-add-location': 'addLocation'
		},
		addLocation: function (e) {
			e.preventDefault();

			var data = lib.syphon.serialize(this);
			this.trigger('weather:location:add', data);
		}
	});

	var locationView = lib.marionette.ItemView.extend({
		template: lib._.template(itemTemplate),
		tagName: 'tr',
		triggers: {
			'click .js-update-location': 'weather:location:update',
			'click .js-delete-location': 'weather:location:delete',
			'click .js-btn-add-tile': 'weather:location:add-tile'
		},
		showSpinner: function () {
			this.$('.js-update-location-spin').removeClass('hidden');
		},
		hideSpinner: function () {
			this.$('.js-update-location-spin').addClass('hidden');
		}
	});

	var locationListView = lib.marionette.CompositeView.extend({
		template: lib._.template(listTemplate),
		childView: locationView,
		childViewContainer: 'tbody'
	});


	return {
		WeatherSettingsLayout: weatherSettingsLayout,
		WeatherSettingsFormView: weatherSettingsFormView,
		LocationView: locationView,
		LocationListView: locationListView
	};
});