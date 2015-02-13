define(['app',
		'webapp/alarm-clock/editor-model',
		'webapp/alarm-clock/editor-view'],
	function (application, models, views) {

		var api = {
			createEditor: function (model) {

				var view = new views.AlarmEditorView({ model: model });

				view.on('alarm-clock:editor:save', api.save);
				view.on('alarm-clock:editor:cancel', api.redirectToList);

				application.setContentView(view);
			},

			edit: function (id) {

				models.loadEditorData(id).done(api.createEditor);
			},

			save: function () {

				var model = this.model;
				models.saveAlarm(model).done(api.redirectToList);
			},
			redirectToList: function () {
				application.navigate('webapp/alarm-clock/list');
			}
		};

		return {
			start: function (id) {
				api.edit(id);
			}
		};
	});