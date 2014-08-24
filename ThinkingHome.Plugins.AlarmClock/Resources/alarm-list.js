define(
	['app', 'common',
		'webapp/alarm-clock/list-model',
		'webapp/alarm-clock/list-view'],
	function (application, commonModule) {

		application.module('AlarmClock.List', function (module, app, backbone, marionette, $, _) {

			var api = {

				addAlarm: function() {
					app.trigger('page:load', 'webapp/alarm-clock/editor');
				},
				
				editAlarm: function (childView) {

					var id = childView.model.get('id');
					app.trigger('page:load', 'webapp/alarm-clock/editor', id);
				},
				
				enable: function (childView) {
					var id = childView.model.get('id');
					app.request('cmd:alarm-clock:set-state', id, true).done(api.reload);
				},
				
				disable: function (childView) {
					var id = childView.model.get('id');
					app.request('cmd:alarm-clock:set-state', id, false).done(api.reload);
				},

				deleteAlarm: function (childView) {

					var name = childView.model.get('name');

					if (commonModule.utils.confirm('Delete the alarm "{0}"?', name)) {

						var id = childView.model.get('id');
						app.request('cmd:alarm-clock:delete', id).done(api.reload);
					}
				},
				
				stopAllSounds: function () {

					app.request('cmd:alarm-clock:stop').done(function() {

						commonModule.utils.alert('All alarm sounds were stopped.');
					});
				},
				
				reload: function () {

					var rq = app.request('query:alarm-clock:list');

					$.when(rq).done(function (items) {

						var view = new module.AlarmListView({ collection: items });

						view.on('alarm-clock:add', api.addAlarm);
						view.on('alarm-clock:stop', api.stopAllSounds);
						view.on('childview:alarm-clock:enable', api.enable);
						view.on('childview:alarm-clock:disable', api.disable);
						view.on('childview:alarm-clock:edit', api.editAlarm);
						view.on('childview:alarm-clock:delete', api.deleteAlarm);
						
						app.setContentView(view);
					});
				}
			};

			module.start = function () {
				api.reload();
			};

		});

		return application.AlarmClock.List;
	});