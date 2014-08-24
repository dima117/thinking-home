define(
	['app', 'application/sections/list-model', 'application/sections/list-view'],
	function (application) {

		application.module('WebUI.Sections', function (module, app, backbone, marionette, $, _) {

			var api = {

				addTile: function (childView) {

					var def = childView.model.get('tileDefKey');
					var data = {};

					if (!def) {

						def = 'ThinkingHome.Plugins.WebUI.AppSectionShortcutTileDefinition';
						data.title = childView.model.get('name');
						data.url = childView.model.get('path');
					}

					app.addTile(def, data);
				},

				reload: function (requestName, pageTitle) {

					app.request(requestName).done(function (items) {

						var view = new module.SectionListView({ collection: items, title: pageTitle });
						view.on('childview:sections:add-tile', api.addTile);

						app.setContentView(view);
					});
				}
			};

			module.api = api;
		});

		return application.WebUI.Sections;
	});