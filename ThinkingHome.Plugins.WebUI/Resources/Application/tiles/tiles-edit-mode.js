define(
	['app', 'common', 'application/tiles/tiles-edit-mode-model', 'application/tiles/tiles-edit-mode-view'],
	function (application, commonModule, models) {

		application.module('WebUI.TilesEditMode', function (module, app, backbone, marionette, $, _) {

			var api = {

				del: function (childView) {

					var title = childView.model.get('title');

					if (commonModule.utils.confirm('Delete the tile "{0}"?', title)) {

						var id = childView.model.get('id');
						models.remove(id).done(api.reload);
					}
				},
				
				sort: function () {

					var collection = this.collection;
					models.saveOrder(collection);
				},

				reload: function () {

					models.load().done(function (collection) {

						var view = new module.TileCollectionViewEditMode({
							collection: collection
						});

						view.on('webui:tile:sort', api.sort);
						view.on('childview:webui:tile:delete', api.del);
						
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