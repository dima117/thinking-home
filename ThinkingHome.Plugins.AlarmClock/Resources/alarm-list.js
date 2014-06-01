define(
	['app',
		'webapp/alarm-clock/list-model',
		'webapp/alarm-clock/list-view'],
	function (application) {

		application.module('AlarmClock.List', function (module, app, backbone, marionette, $, _) {

			var api = {
				openEditor: function (model) {
					
					var view = new module.AlarmEditorView({ model: model });
					view.on('alarm-clock:editor:save', api.save);
					view.on('alarm-clock:editor:cancel', api.reload);

					app.setContentView(view);
				},

				add: function() {

					var model = new module.AlarmListItem();
					api.openEditor(model);
				},
				
				edit: function(itemView) {

					var model = itemView.model;
					api.openEditor(model);
				},

				save: function () {

					var model = this.model;
					app.request('update:alarm-clock:save', model).done(api.reload);
				},

				setState: function (itemView, enabled) {

					var id = itemView.model.get('id');
					app.request('update:alarm-clock:set-state', id, enabled).done(api.reload);
				},
				
				stop: function () {

					app.request('update:alarm-clock:stop').done(function() {

						app.Common.utils.alert('All alarm has been stopped.');
					});
				},
				
				reload: function () {

					var rq = app.request('load:alarm-clock:list');

					$.when(rq).done(function (items) {

						var view = new module.AlarmListView({ collection: items });

						view.on('alarm-clock:add', api.add);
						view.on('alarm-clock:stop', api.stop);
						view.on('itemview:alarm-clock:set-state', api.setState);
						view.on('itemview:alarm-clock:edit', api.edit);
						
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