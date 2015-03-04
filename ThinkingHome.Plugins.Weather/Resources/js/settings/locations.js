define(
	['app', 'common',
		'webapp/weather/locations-model',
		'webapp/weather/locations-view'],
	function (application, commonModule, models) {

		application.module('Weather.Settings', function (module, app, backbone, marionette, $, _) {

			var layoutView;

			var api = {

				addWeatherTile: function (view) {

					var locationId = view.model.get('id');
					app.addTile('ThinkingHome.Plugins.Weather.WeatherTileDefinition', { cityId: locationId });
				},

				addLocation: function () {

					var displayName = this.model.get('displayName');
					var query = this.model.get('query');

					if (displayName && query) {

						models.addLocation(displayName, query).done(api.reloadList);
					}
				},

				deleteLocation: function (childView) {

					var displayName = childView.model.get('displayName');

					if (commonModule.utils.confirm('Delete the location "{0}" and all location data?', displayName)) {

						var locationId = childView.model.get('id');

						models.deleteLocation(locationId).done(api.reloadList);
					}
				},

				updateLocation: function (childView) {

					var locationId = childView.model.get('id');

					childView.showSpinner();

					models.updateLocation(locationId)
						.done(function () {
							childView.hideSpinner();
						});
				},

				reloadForm: function () {

					var formData = new models.Location();

					var form = new module.WeatherSettingsFormView({ model: formData });
					form.on('weather:location:add', api.addLocation);
					layoutView.regionForm.show(form);
				},

				reloadList: function () {

					models.loadLocations()
						.done(function (list) {

							var view = new module.LocationListView({ collection: list });
							view.on('childview:weather:location:delete', api.deleteLocation);
							view.on('childview:weather:location:update', api.updateLocation);
							view.on('childview:weather:location:add-tile', api.addWeatherTile);

							layoutView.regionList.show(view);
						});
				}
			};

			module.start = function () {

				// init layout
				layoutView = new module.WeatherSettingsLayout();
				app.setContentView(layoutView);

				api.reloadForm();
				api.reloadList();
			};

		});

		return application.Weather.Settings;
	});