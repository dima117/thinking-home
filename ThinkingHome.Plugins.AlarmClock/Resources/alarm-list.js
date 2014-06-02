define(
	['app',
		'webapp/alarm-clock/list-model',
		'webapp/alarm-clock/list-view'],
	function (application) {

		application.module('AlarmClock.List', function (module, app, backbone, marionette, $, _) {

			var api = {
				openEditor: function (alarmId) {

					app.request('load:alarm-clock:editor', alarmId).done(function (model) {
					
						var view = new module.AlarmEditorView({ model: model });
						view.on('alarm-clock:editor:save', api.saveAlarm);
						view.on('alarm-clock:editor:cancel', api.reload);

						app.setContentView(view);
					});
				},

				addAlarm: function() {
					api.openEditor();
				},
				
				editAlarm: function (itemView) {

					var alarmId = itemView.model.get('id');
					api.openEditor(alarmId);
				},

				saveAlarm: function () {

					var model = this.model;
					app.request('update:alarm-clock:save', model).done(api.reload);
				},

				deleteAlarm: function (itemView) {

					var name = itemView.model.get('name');

					if (app.Common.utils.confirm('Delete the alarm "{0}"?', name)) {

						var id = itemView.model.get('id');
						app.request('update:alarm-clock:delete', id).done(api.reload);
					}
				},

				setState: function (itemView, enabled) {

					var id = itemView.model.get('id');
					app.request('update:alarm-clock:set-state', id, enabled).done(api.reload);
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
						view.on('itemview:alarm-clock:set-state', api.setState);
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