define(
	['app',
		'common',
		'webapp/scripts/script-list-model',
		'webapp/scripts/script-list-view'],
	function (application, commonModule, models, views) {

		application.module('Scripts.List', function (module, app, backbone, marionette, $, _) {

			var api = {

				runScript: function (view) {

					var scriptId = view.model.get('id');

					models.runScript(scriptId).done(function () {

						var name = view.model.get('name');
						commonModule.utils.alert('The script "{0}" has been executed.', name);
					});
				},

				deleteScript: function (view) {

					var scriptName = view.model.get('name');

					if (commonModule.utils.confirm('Delete the script "{0}"?', scriptName)) {

						var scriptId = view.model.get('id');

						models.deleteScript(scriptId).done(api.reload);
					}
				},

				addScriptTile: function (view) {

					var scriptId = view.model.get('id');
					app.addTile('ThinkingHome.Plugins.Scripts.ScriptsTileDefinition', { id: scriptId });
				},

				addScript: function () {

					app.navigate('webapp/scripts/script-editor');
				},
				editScript: function (childView) {

					var scriptId = childView.model.get('id');
					app.navigate('webapp/scripts/script-editor', scriptId);
				},
				reload: function () {

					models.loadScriptList()
						.done(function (items) {

							var view = new views.ScriptListView({ collection: items });

							view.on('scripts:add', api.addScript);
							view.on('childview:scripts:edit', api.editScript);
							view.on('childview:scripts:run', api.runScript);
							view.on('childview:scripts:delete', api.deleteScript);
							view.on('childview:scripts:add-tile', api.addScriptTile);

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