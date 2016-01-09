define(
	['lib',
		'webapp/weather/locations-model',
		'webapp/weather/locations-view',
		'lang!webapp/weather/lang.json'],
	function (lib, models, views, lang) {

		var locationList = lib.common.AppSection.extend({
			start: function () {
				this.layout = new views.WeatherSettingsLayout();
				this.application.setContentView(this.layout);

				this.reloadForm();
				this.reloadList();
			},

			addLocation: function (data) {
				if (data.displayName && data.query) {
					models.addLocation(data.displayName, data.query).done(this.bind('reloadList'));
				}
			},

			deleteLocation: function (childView) {
				var displayName = childView.model.get('displayName');

				if (lib.utils.confirm(lang.get('Delete_the_location_0_and_all_location_data'), displayName)) {
					var locationId = childView.model.get('id');
					models.deleteLocation(locationId).done(this.bind('reloadList'));
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

				this.listenTo(form, 'weather:location:add', this.bind('addLocation'));
				this.layout.regionForm.show(form);
			},

			displayList: function (list) {
				var view = new views.LocationListView({ collection: list });
				this.listenTo(view, 'childview:weather:location:delete', this.bind('deleteLocation'));
				this.listenTo(view, 'childview:weather:location:update', this.bind('updateLocation'));

				this.layout.regionList.show(view);
			},

			reloadList: function () {
				models.loadLocations().done(this.bind('displayList'));
			}
		});

		return locationList;
	});