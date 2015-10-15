define(['lib',
		'webapp/alarm-clock/editor-model',
		'webapp/alarm-clock/editor-view'],
	function (lib, models, views) {

		var alarmEditor = lib.common.AppSection.extend({
			start: function (id) {
				models.loadEditorData(id).done(this.bind('createEditor'));
			},

			createEditor: function (model) {
				var view = new views.AlarmEditorView({ model: model });

				this.listenTo(view, 'alarm-clock:editor:save', this.bind('save', view));
				this.listenTo(view, 'alarm-clock:editor:delete', this.bind('deleteAlarm', view));
				this.listenTo(view, 'alarm-clock:editor:cancel', this.bind('redirectToList'));

				this.application.setContentView(view);
			},

			save: function (view) {
				var model = view.model.toJSON();
				models.saveAlarm(model).done(this.bind('redirectToList'));
			},

			deleteAlarm: function (view) {
				var name = view.model.get('name');

				if (lib.utils.confirm('Do you want to delete the alarm?', name)) {
					var id = view.model.get('id');
					models.deleteAlarm(id).done(this.bind('redirectToList'));
				}
			},

			redirectToList: function () {
				this.application.navigate('webapp/alarm-clock/list');
			}
		});

		return alarmEditor;
	});