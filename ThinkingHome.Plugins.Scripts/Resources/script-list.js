define(
	['app',
		'common',
		'webapp/scripts/script-list-model',
		'webapp/scripts/script-list-view'],
	function (application, common, models, views) {

		var api = {
			runScript: function (view) {

				var scriptId = view.model.get('id');

				models.runScript(scriptId).done(function () {

					var name = view.model.get('name');
					common.utils.alert('The script "{0}" has been executed.', name);
				});
			},

			deleteScript: function (view) {

				var scriptName = view.model.get('name');

				if (common.utils.confirm('Delete the script "{0}"?', scriptName)) {

					var scriptId = view.model.get('id');

					models.deleteScript(scriptId).done(api.reload);
				}
			},

			addScriptTile: function (view) {

				var scriptId = view.model.get('id');
				application.addTile('ThinkingHome.Plugins.Scripts.ScriptsTileDefinition', { id: scriptId });
			},

			addScript: function () {

				application.navigate('webapp/scripts/script-editor');
			},
			editScript: function (childView) {

				var scriptId = childView.model.get('id');
				application.navigate('webapp/scripts/script-editor', scriptId);
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

						application.setContentView(view);
					});
			}
		};

		return {
			start: function () {
				api.reload();
			}
		};
	});