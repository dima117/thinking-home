define(['app'], function (application) {

	application.module('AlarmClock.List', function (module, app, backbone, marionette, $, _) {

		// entities
		module.AlarmListItem = backbone.Model.extend();

		module.AlarmCollection = backbone.Collection.extend({
			model: module.AlarmListItem
		});

		// api
		var api = {

			loadList: function () {

				var defer = $.Deferred();

				$.getJSON('/api/alarm-clock/list')
					.done(function (items) {
						var collection = new module.AlarmCollection(items);
						defer.resolve(collection);
					})
					.fail(function () {

						defer.resolve(undefined);
					});

				return defer.promise();
			}
		};

		// requests
		app.reqres.setHandler('load:alarm-clock:list', function () {
			return api.loadList();
		});
	});

	return application.AlarmClock.List;
});