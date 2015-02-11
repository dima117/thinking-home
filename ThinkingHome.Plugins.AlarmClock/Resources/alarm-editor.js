define(
	['app',
		'webapp/alarm-clock/editor-model',
		'webapp/alarm-clock/editor-view'],
	function (application, models) {

		application.module('AlarmClock.Editor', function (module, app, backbone, marionette, $, _) {

			var api = {
				createEditor: function (model) {

					var view = new module.AlarmEditorView({ model: model });

					view.on('alarm-clock:editor:save', api.save);
					view.on('alarm-clock:editor:cancel', api.redirectToList);

					app.setContentView(view);
				},
				
				edit: function (id) {
					
					models.loadEditorData(id).done(api.createEditor);
				},

				save: function () {

					var model = this.model;
					models.saveAlarm(model).done(api.redirectToList);
				},
				redirectToList: function () {
					app.navigate('webapp/alarm-clock/list');
				}
			};

			module.start = function (id) {
				api.edit(id);
			};

		});

		return application.AlarmClock.Editor;
	});