define(
	['app', 'application/sections/list-model', 'application/sections/list-view'],
	function (application) {

		application.module('WebUI.Sections', function (module, app, backbone, marionette, $, _) {

			var api = {

				addTile: function (itemView) {

					var def = itemView.model.get('tileDefKey');
					var data = {};

					if (!def) {

						def = 'ThinkingHome.Plugins.WebUI.AppSectionShortcutTileDefinition';
						data.title = itemView.model.get('name');
						data.url = itemView.model.get('path');
					}

					app.addTile(def, data);
				},

				reload: function (requestName, pageTitle) {

					app.request(requestName).done(function (items) {

						var view = new module.SectionListView({ collection: items, title: pageTitle });
						view.on('itemview:sections:add-tile', api.addTile);

						app.setContentView(view);
					});
				}
			};

			module.api = api;
		});

		return application.WebUI.Sections;
	});