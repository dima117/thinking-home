define(
	['app', 'lib',
		'webapp/alarm-clock/list-model',
		'webapp/alarm-clock/list-view'],
	function (application, lib, models, views) {

		var api = {

			addAlarm: function () {
				application.navigate('webapp/alarm-clock/editor');
			},

			editAlarm: function (childView) {

				var id = childView.model.get('id');
				application.navigate('webapp/alarm-clock/editor', id);
			},

			enable: function (childView) {
				var id = childView.model.get('id');
				models.setState(id, true).done(api.reload);
			},

			disable: function (childView) {
				var id = childView.model.get('id');
				models.setState(id, false).done(api.reload);
			},

			stopAllSounds: function () {

				models.stopAlarm().done(function () {

					lib.utils.alert('All alarm sounds were stopped.');
				});
			},

			reload: function () {

				models.loadList().done(function (items) {

					var view = new views.AlarmListView({ collection: items });

					view.on('alarm-clock:add', api.addAlarm);
					view.on('alarm-clock:stop', api.stopAllSounds);
					view.on('childview:alarm-clock:enable', api.enable);
					view.on('childview:alarm-clock:disable', api.disable);
					view.on('childview:alarm-clock:edit', api.editAlarm);

					application.setContentView(view);
				});
			}
		};

		return {
			start: function () {
				api.reload();
			}
		};
	});