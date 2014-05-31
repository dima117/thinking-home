define(
	['app', 'webapp/scripts/script-editor-model', 'webapp/scripts/script-editor-view'],
	function (application) {

		application.module('Scripts.Editor', function (module, app, backbone, marionette, $, _) {

			var api = {

				createEditor: function (model) {

					var view = new module.ScriptEditorView({ model: model });

					view.on('scripts:editor:cancel', api.redirectToList);
					view.on('scripts:editor:save', api.save);

					app.setContentView(view);
				},

				redirectToList: function () {
					app.trigger('page:load', 'webapp/scripts/script-list');
				},

				save: function (data) {

					this.model.set(data);

					app.request('update:scripts:editor:save', this.model)
						.done(api.redirectToList);
				},

				edit: function (scriptId) {

					app.request('load:scripts:editor:load', scriptId)
						.done(api.createEditor);
				},

				add: function () {

					var name = window.prompt('Enter script name:', '');

					if (name) {

						var model = new module.ScriptData({ name: name });
						api.createEditor(model);
					}
				}
			};

			module.start = function (scriptId) {

				if (scriptId) {
					api.edit(scriptId);
				} else {
					api.add();
				}
			};

		});

		return application.Scripts.Editor;
	});