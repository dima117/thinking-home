define(
	['app', 'webapp/scripts/script-editor-model', 'webapp/scripts/script-editor-view'],
	function (application) {

		application.module('Scripts.Editor', function (module, app, backbone, marionette, $, _) {

			var api = {

				redirectToList: function () {
					module.trigger('subapp:open', 'list');
				},

				save: function (data) {

					this.model.set(data);

					app.request('update:scripts:editor:save', this.model)
						.done(api.redirectToList);
				},

				load: function (scriptId) {

					var rq = app.request('load:scripts:editor:load', scriptId);

					$.when(rq).done(function (model) {

						var view = new module.ScriptEditorView({ model: model });

						view.on('scripts:editor:cancel', api.redirectToList);
						view.on('scripts:editor:save', api.save);

						app.setContentView(view);
					});
				}
			};

			module.start = function (scriptId) {
				api.load(scriptId);
			};

		});

		return application.Scripts.Editor;
	});