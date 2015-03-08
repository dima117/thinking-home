define(
	['app', 'application/sections/list-model', 'application/sections/list-view'],
	function (application, models, views) {

		var api = {

			addTile: function (childView) {

				var def = childView.model.get('tileDefKey');
				var data = {};

				if (!def) {

					def = 'ThinkingHome.Plugins.WebUI.AppSectionShortcutTileDefinition';
					data.title = childView.model.get('name');
					data.url = childView.model.get('path');
				}

				application.addTile(def, data);
			},

			navigate: function (childView) {

				var path = childView.model.get('path');
				application.navigate(path);
			},

			reload: function (requestName, pageTitle) {

				// todo: переписать выбор метода для загрузки списка
				models[requestName]().done(function (items) {

					var view = new views.SectionListView({ collection: items, title: pageTitle });
					view.on('childview:sections:add-tile', api.addTile);
					view.on('childview:sections:navigate', api.navigate);

					application.setContentView(view);
				});
			}
		};

		return {
			api: api
		};
	});