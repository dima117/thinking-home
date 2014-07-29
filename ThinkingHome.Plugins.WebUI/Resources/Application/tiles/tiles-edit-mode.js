define(
	['app', 'common', 'application/tiles/tiles-edit-mode-model', 'application/tiles/tiles-edit-mode-view'],
	function (application, commonModule) {

		application.module('WebUI.TilesEditMode', function (module, app, backbone, marionette, $, _) {

			var api = {

				del: function (itemView) {

					var title = itemView.model.get('title');

					if (commonModule.utils.confirm('Delete the tile "{0}"?', title)) {

						var id = itemView.model.get('id');
						app.request('cmd:tiles:edit-mode-delete', id).done(api.reload);
					}
				},
				
				sort: function () {

					var collection = this.collection;
					app.request('cmd:tiles:edit-mode-sort', collection);
				},

				reload: function () {

					app.request('query:tiles:edit-mode-list').done(function (collection) {

						var view = new module.TileCollectionViewEditMode({
							collection: collection
						});

						view.on('webui:tile:sort', api.sort);
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