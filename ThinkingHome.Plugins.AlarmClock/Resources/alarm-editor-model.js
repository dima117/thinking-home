define(['lib'], function (lib) {

	// entities
	var alarmEditorModel = lib.backbone.Model.extend();

	// api
	var api = {

		loadEditorData: function (id) {

			var defer = lib.$.Deferred();

			lib.$.getJSON('/api/alarm-clock/editor', { id: id })
				.done(function (alarm) {
					var model = new alarmEditorModel(alarm);
					defer.resolve(model);
				})
				.fail(function () {
					defer.resolve(undefined);
				});

			return defer.promise();
		},
		saveAlarm: function (model) {

			return lib.$.post('/api/alarm-clock/save', model.toJSON()).promise();
		}
	};

	return {
		model: alarmEditorModel,
		loadEditorData: api.loadEditorData,
		saveAlarm: api.saveAlarm
	};
});