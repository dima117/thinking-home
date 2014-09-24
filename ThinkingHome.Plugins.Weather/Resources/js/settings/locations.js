define(
	['app', 'common',
		'webapp/weather/locations-model',
		'webapp/weather/locations-view'],
	function (application, commonModule) {

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

						app.request('cmd:weather:locations-add', displayName, query)
							.done(api.reloadList);
					}
				},

				deleteLocation: function (childView) {

					var displayName = childView.model.get('displayName');

					if (commonModule.utils.confirm('Delete the location "{0}" and all location data?', displayName)) {

						var locationId = childView.model.get('id');

						app.request('cmd:weather:locations-delete', locationId)
							.done(api.reloadList);
					}
				},

				updateLocation: function (childView) {

					var locationId = childView.model.get('id');

					childView.showSpinner();

					app.request('cmd:weather:locations-update', locationId)
						.done(function () {
							childView.hideSpinner();
						});
				},

				reloadForm: function () {

					//app.request('query:scripts:subscription-form')
					//	.done(function (formData) {

					var formData = new module.Location();

					var form = new module.WeatherSettingsFormView({ model: formData });
					form.on('weather:location:add', api.addLocation);
					layoutView.regionForm.show(form);
					//});
				},

				reloadList: function () {

					app.request('query:weather:locations')
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