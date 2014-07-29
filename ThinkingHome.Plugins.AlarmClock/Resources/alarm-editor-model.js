define(['app'], function (application) {

	application.module('AlarmClock.Editor', function (module, app, backbone, marionette, $, _) {

		// entities
		module.AlarmEditorModel = backbone.Model.extend();

		// api
		var api = {

			loadEditor: function (id) {
			
				var defer = $.Deferred();

				$.getJSON('/api/alarm-clock/editor', { id: id })
					.done(function (alarm) {
						var model = new module.AlarmEditorModel(alarm);
						defer.resolve(model);
					})
					.fail(function () {
						defer.resolve(undefined);
					});

				return defer.promise();
			},
			saveAlarm: function (model) {

				return $.post('/api/alarm-clock/save', model.toJSON()).promise();
			}
		};

		// requests
		app.reqres.setHandler('query:alarm-clock:editor', api.loadEditor);
		app.reqres.setHandler('cmd:alarm-clock:save', api.saveAlarm);
	});

	return application.AlarmClock.Editor;
});