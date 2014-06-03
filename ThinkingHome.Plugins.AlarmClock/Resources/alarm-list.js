define(
	['app',
		'webapp/alarm-clock/list-model',
		'webapp/alarm-clock/list-view'],
	function (application) {

		application.module('AlarmClock.List', function (module, app, backbone, marionette, $, _) {

			var api = {

				addAlarm: function() {
					app.trigger('page:load', 'webapp/alarm-clock/editor');
				},
				
				editAlarm: function (itemView) {

					var id = itemView.model.get('id');
					app.trigger('page:load', 'webapp/alarm-clock/editor', id);
				},
				
				enable: function (itemView) {
					var id = itemView.model.get('id');
					app.request('update:alarm-clock:set-state', id, true).done(api.reload);
				},
				
				disable: function (itemView) {
					var id = itemView.model.get('id');
					app.request('update:alarm-clock:set-state', id, false).done(api.reload);
				},

				deleteAlarm: function (itemView) {

					var name = itemView.model.get('name');

					if (app.Common.utils.confirm('Delete the alarm "{0}"?', name)) {

						var id = itemView.model.get('id');
						app.request('update:alarm-clock:delete', id).done(api.reload);
					}
				},
				
				stopAllSounds: function () {

					app.request('update:alarm-clock:stop').done(function() {

						app.Common.utils.alert('All alarm sounds were stopped.');
					});
				},
				
				reload: function () {

					var rq = app.request('load:alarm-clock:list');

					$.when(rq).done(function (items) {

						var view = new module.AlarmListView({ collection: items });

						view.on('alarm-clock:add', api.addAlarm);
						view.on('alarm-clock:stop', api.stopAllSounds);
						view.on('itemview:alarm-clock:enable', api.enable);
						view.on('itemview:alarm-clock:disable', api.disable);
						view.on('itemview:alarm-clock:edit', api.editAlarm);
						view.on('itemview:alarm-clock:delete', api.deleteAlarm);
						
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