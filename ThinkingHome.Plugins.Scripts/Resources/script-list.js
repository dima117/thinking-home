define(
	['app',
		'webapp/scripts/script-list-model',
		'webapp/scripts/script-list-view'],
	function (application) {

		application.module('Scripts.List', function (module, app, backbone, marionette, $, _) {

			var api = {

				runScript: function (view) {

					var scriptId = view.model.get('id');

					app.request('update:scripts:run', scriptId).done(function () {

						var name = view.model.get('name');
						app.Common.utils.alert('Script "{0}" has been executed.', name);
					});
				},

				openEditor: function (itemView) {

					var scriptId = itemView.model.get('id');
					app.trigger('page:load', 'webapp/scripts/script-editor', scriptId);
				},
				reload: function () {

					var rq = app.request('load:scripts:list');

					$.when(rq).done(function (items) {

						var view = new module.ScriptListView({ collection: items });

						view.on('itemview:scripts:edit', api.openEditor);
						view.on('itemview:scripts:run', api.runScript);

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