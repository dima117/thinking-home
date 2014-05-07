define(
	['app',
		'webapp/scripts/script-editor',
		'webapp/scripts/script-list-model',
		'webapp/scripts/script-list-view'],
	function (application, editor) {

		application.module('Scripts.List', function (module, app, backbone, marionette, $, _) {

			var mainView = new module.ScriptListLayout();

			var api = {

				openEditor: function (itemView) {
					
					var scriptId = itemView.model.get('id');
					var editorView = editor.createView(scriptId);
					mainView.regionList.show(editorView);
				},
				reload: function () {

					var rq = app.request('load:scripts:list');
					
					$.when(rq).done(function (items) {

						var listView = new module.ScriptListView({ collection: items });

						listView.on('itemview:scripts:edit', api.openEditor);
						//listView.on('itemview:packages:uninstall', api.uninstall);

						mainView.regionList.show(listView);
					});
				}
			};

			module.createView = function () {

				api.reload();
				return mainView;
			};

		});

		return application.Scripts.List;
	});