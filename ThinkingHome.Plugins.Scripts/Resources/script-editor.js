define(
	['app', 'webapp/scripts/script-editor-model', 'webapp/scripts/script-editor-view'],
	function (application) {

		application.module('Scripts.Editor', function (module, app, backbone, marionette, $, _) {

			var api = {

				load: function (scriptId) {

					var rq = app.request('load:scripts:editor:load', scriptId);

					$.when(rq).done(function (model) {

						var view = new module.ScriptEditorView({ model: model });

						view.on('scripts:editor:cancel', function() {
							module.trigger('subapp:open', 'list');
						});

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