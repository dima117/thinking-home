define(
	['app', 'common', 'application/tiles/tiles-edit-mode-model', 'application/tiles/tiles-edit-mode-view'],
	function (application, common, models, views) {

		var api = {

			del: function (childView) {

				var title = childView.model.get('title');

				if (common.utils.confirm('Delete the tile "{0}"?', title)) {

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

					var view = new views.TileCollectionViewEditMode({
						collection: collection
					});

					view.on('webui:tile:sort', api.sort);
					view.on('childview:webui:tile:delete', api.del);

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