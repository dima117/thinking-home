define(
	['app', 'webapp/scripts/script-editor-model', 'webapp/scripts/script-editor-view'],
	function (application) {

		application.module('Scripts.Editor', function (module, app, backbone, marionette, $, _) {

			var mainView = new module.ScriptEditorLayout();

			var api = {

				load: function (scriptId) {

					var rq = app.request('load:scripts:editor:load', scriptId);

					$.when(rq).done(function (model) {

						var editorView = new module.ScriptEditorView({ model: model });

						//listView.on('itemview:scripts:edit', api.openEditor);
						//listView.on('itemview:packages:uninstall', api.uninstall);

						mainView.regionContent.show(editorView);
					});
				}
			};

			module.createView = function (scriptId) {

				api.load(scriptId);
				return mainView;
			};

		});

		return application.Scripts.Editor;
	});