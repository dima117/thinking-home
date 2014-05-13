define(
	['app',
		'webapp/scripts/script-list-model',
		'webapp/scripts/script-list-view'],
	function (application) {

		application.module('Scripts.List', function (module, app, backbone, marionette, $, _) {

			var api = {

				openEditor: function (itemView) {

					var scriptId = itemView.model.get('id');
					module.trigger('subapp:open', 'editor', scriptId);
				},
				reload: function () {

					var rq = app.request('load:scripts:list');

					$.when(rq).done(function (items) {

						var view = new module.ScriptListView({ collection: items });

						view.on('itemview:scripts:edit', api.openEditor);
						//listView.on('itemview:packages:uninstall', api.uninstall);
						app.setContentView(view);
					});
				}
			};

			module.start = function () {
				api.reload();
			};

		});

		return application.Scripts.List;
	});