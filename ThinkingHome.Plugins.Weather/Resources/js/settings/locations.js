define(
	['app',
		'webapp/weather/locations-model',
		'webapp/weather/locations-view'],
	function (application) {

		application.module('Weather.Settings', function (module, app, backbone, marionette, $, _) {

			var layoutView;

			var api = {

				addLocation: function () {
					alert('add location');
				},

				deleteLocation: function () {
					alert('delete location');
				},

				updateLocation: function (itemView) {

					var locationId = itemView.model.get('id');

					itemView.showSpinner();

					app.request('update:weather:locations-update', locationId)
						.done(function() {
							itemView.hideSpinner();
						});
				},

				reloadForm: function () {

					//app.request('load:scripts:subscription-form')
					//	.done(function (formData) {

					var form = new module.WeatherSettingsFormView();//({ model: formData });
					form.on('weather:location:add', api.addLocation);
					layoutView.regionForm.show(form);
					//});
				},

				reloadList: function () {

					app.request('load:weather:locations')
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