define(
	['app', 'common',
		'webapp/weather/locations-model',
		'webapp/weather/locations-view'],
	function (application, commonModule) {

		application.module('Weather.Settings', function (module, app, backbone, marionette, $, _) {

			var layoutView;

			var api = {

				addLocation: function () {

					var displayName = this.model.get('displayName');
					var query = this.model.get('query');

					if (displayName && query) {

						app.request('cmd:weather:locations-add', displayName, query)
							.done(api.reloadList);
					}
				},

				deleteLocation: function (itemView) {

					var displayName = itemView.model.get('displayName');

					if (commonModule.utils.confirm('Delete the location "{0}" and all location data?', displayName)) {

						var locationId = itemView.model.get('id');

						app.request('cmd:weather:locations-delete', locationId)
							.done(api.reloadList);
					}
				},

				updateLocation: function (itemView) {

					var locationId = itemView.model.get('id');

					itemView.showSpinner();

					app.request('cmd:weather:locations-update', locationId)
						.done(function () {
							itemView.hideSpinner();
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
							view.on('itemview:weather:location:delete', api.deleteLocation);
							view.on('itemview:weather:location:update', api.updateLocation);
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