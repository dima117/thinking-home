define(
	['app', 'lib',
		'webapp/weather/locations-model',
		'webapp/weather/locations-view'],
	function (application, lib, models, views) {

		var layoutView;

		var api = {

			addLocation: function (data) {

				if (data.displayName && data.query) {

					models.addLocation(data.displayName, data.query).done(api.reloadList);
				}
			},

			deleteLocation: function (childView) {

				var displayName = childView.model.get('displayName');

				if (lib.utils.confirm('Delete the location "{0}" and all location data?', displayName)) {

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

				var form = new views.WeatherSettingsFormView({ model: formData });
				form.on('weather:location:add', api.addLocation);
				layoutView.regionForm.show(form);
			},

			reloadList: function () {

				models.loadLocations()
					.done(function (list) {

						var view = new views.LocationListView({ collection: list });
						view.on('childview:weather:location:delete', api.deleteLocation);
						view.on('childview:weather:location:update', api.updateLocation);

						layoutView.regionList.show(view);
					});
			}
		};

		return {
			start: function () {

				// init layout
				layoutView = new views.WeatherSettingsLayout();
				application.setContentView(layoutView);

				api.reloadForm();
				api.reloadList();
			}
		};
	});