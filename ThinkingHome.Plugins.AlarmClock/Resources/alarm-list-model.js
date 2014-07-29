define(['app'], function (application) {

	application.module('AlarmClock.List', function (module, app, backbone, marionette, $, _) {

		// entities
		module.AlarmListItem = backbone.Model.extend({
			defaults: {
				hours: 0,
				minutes: 0
			}
		});

		module.AlarmCollection = backbone.Collection.extend({
			model: module.AlarmListItem,
			comparator: function(alarm) {
				return alarm.get("hours") * 60 + alarm.get("minutes");
			}
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
			},
			setState: function (id, enabled) {

				return $.post('/api/alarm-clock/set-state', { id: id, enabled: enabled }).promise();
			},
			deleteAlarm: function (id) {

				return $.post('/api/alarm-clock/delete', { id: id }).promise();
			},
			stopAlarm: function () {

				return $.post('/api/alarm-clock/stop').promise();
			}
		};

		// requests
		app.reqres.setHandler('query:alarm-clock:list', api.loadList);
		app.reqres.setHandler('cmd:alarm-clock:set-state', api.setState);
		app.reqres.setHandler('cmd:alarm-clock:stop', api.stopAlarm);
		app.reqres.setHandler('cmd:alarm-clock:delete', api.deleteAlarm);
	});

	return application.AlarmClock.List;
});