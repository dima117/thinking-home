define(
	['app',
		'webapp/alarm-clock/editor-model',
		'webapp/alarm-clock/editor-view'],
	function (application) {

		application.module('AlarmClock.Editor', function (module, app, backbone, marionette, $, _) {

			var api = {
				createEditor: function (model) {

					var view = new module.AlarmEditorView({ model: model });

					view.on('alarm-clock:editor:save', api.save);
					view.on('alarm-clock:editor:cancel', api.redirectToList);

					app.setContentView(view);
				},
				
				edit: function (id) {
					app.request('query:alarm-clock:editor', id).done(api.createEditor);
				},

				save: function () {

					var model = this.model;
					app.request('cmd:alarm-clock:save', model).done(api.redirectToList);
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