define(['lib'], function (lib) {

	// entities
	var alarmListItem = lib.backbone.Model.extend({
		defaults: {
			hours: 0,
			minutes: 0
		}
	});

	var alarmCollection = lib.backbone.Collection.extend({
		model: alarmListItem,
		comparator: function (alarm) {
			return alarm.get("hours") * 60 + alarm.get("minutes");
		}
	});

	// api
	var api = {

		loadList: function () {

			var defer = lib.$.Deferred();

			lib.$.getJSON('/api/alarm-clock/list')
				.done(function (items) {
					var collection = new alarmCollection(items);
					defer.resolve(collection);
				})
				.fail(function () {

					defer.resolve(undefined);
				});

			return defer.promise();
		},
		setState: function (id, enabled) {

			return lib.$.post('/api/alarm-clock/set-state', { id: id, enabled: enabled }).promise();
		},
		stopAlarm: function () {

			return lib.$.post('/api/alarm-clock/stop').promise();
		}
	};

	return {
		// model
		AlarmListItem: alarmListItem,
		AlarmCollection: alarmCollection,

		// requests
		loadList: api.loadList,
		setState: api.setState,
		stopAlarm: api.stopAlarm
	};
});