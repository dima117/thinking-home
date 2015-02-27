define(
	['app', 'webapp/scripts/script-editor-model', 'webapp/scripts/script-editor-view'],
	function (application, models, views) {

		var api = {

			createEditor: function (model) {

				var view = new views.ScriptEditorView({ model: model });

				view.on('scripts:editor:cancel', api.redirectToList);
				view.on('scripts:editor:save', api.save);

				application.setContentView(view);
			},

			redirectToList: function () {
				application.navigate('webapp/scripts/script-list');
			},

			save: function (data) {

				this.model.set(data);

				models.saveScript(this.model).done(api.redirectToList);
			},

			edit: function (scriptId) {

				models.loadScript(scriptId).done(api.createEditor);
			},

			add: function () {

				var name = window.prompt('Enter script name:', '');

				if (name) {

					var model = new models.ScriptData({ name: name });
					api.createEditor(model);
				}
			}
		};

		return {
			start: function (scriptId) {

				if (scriptId) {
					api.edit(scriptId);
				} else {
					api.add();
				}
			}
		};
	});