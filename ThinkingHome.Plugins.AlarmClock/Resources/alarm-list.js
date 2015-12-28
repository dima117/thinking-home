define(
	['lib',
		'webapp/alarm-clock/list-model',
		'webapp/alarm-clock/list-view',
		'lang!/webapp/alarm-clock/lang.json'],
	function (lib, models, views, lang) {

		var alarmList = lib.common.AppSection.extend({
			start: function () {
				this.reload();
			},

			addAlarm: function () {
				this.application.navigate('webapp/alarm-clock/editor');
			},

			editAlarm: function (childView) {
				var id = childView.model.get('id');
				this.application.navigate('webapp/alarm-clock/editor', id);
			},

			enable: function (childView) {
				var id = childView.model.get('id');
				models.setState(id, true).done(this.bind('reload'));
			},

			disable: function (childView) {
				var id = childView.model.get('id');
				models.setState(id, false).done(this.bind('reload'));
			},

			stopAllSounds: function () {
				models.stopAlarm().done(function () {
					lib.utils.alert(lang.get('All_alarm_sounds_were_stopped'));
				});
			},

			displayList: function (items) {
				var view = new views.AlarmListView({ collection: items });

				this.listenTo(view, 'alarm-clock:add', this.bind('addAlarm'));
				this.listenTo(view, 'alarm-clock:stop', this.bind('stopAllSounds'));
				this.listenTo(view, 'childview:alarm-clock:enable', this.bind('enable'));
				this.listenTo(view, 'childview:alarm-clock:disable', this.bind('disable'));
				this.listenTo(view, 'childview:alarm-clock:edit', this.bind('editAlarm'));

				this.application.setContentView(view);
			},

			reload: function () {
				models.loadList().done(this.bind('displayList'));
			}
		});

		return alarmList;
	});