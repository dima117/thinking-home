define(
	['app', 'common', 'application/tiles/tiles-edit-mode-model', 'application/tiles/tiles-edit-mode-view'],
	function (application, commonModule) {

		application.module('WebUI.TilesEditMode', function (module, app, backbone, marionette, $, _) {

			var api = {

				del: function (itemView) {

					var title = itemView.model.get('title');

					if (commonModule.utils.confirm('Delete the tile "{0}"?', title)) {

						var id = itemView.model.get('id');
						app.request('update:tiles:edit-mode-delete', id).done(api.reload);
					}
				},

				reload: function () {

					app.request('load:tiles:edit-mode-list').done(function (collection) {

						var view = new module.TileCollectionViewEditMode({
							collection: collection
						});

						view.on('itemview:webui:tile:delete', api.del);
						
						app.setContentView(view);
					});
				}
			};

			module.start = function () {
				api.reload();
			};
		});

		return application.WebUI.TilesEditMode;
	});