define(
	['app', 'application/tiles/tiles-model', 'application/tiles/tiles-view'],
	function (application, models) {

		application.module('WebUI.Tiles', function (module, app, backbone, marionette, $, _) {

			var api = {

				open: function (childView) {

					var id = childView.model.get('id');
					var url = childView.model.get('url');

					if (url) {

						var args = childView.model.get('parameters');
						app.loadPath(url, args);

					} else {

						models.action(id).done(api.done);
					}
				},

				done: function (message) {

					if (message) {

						alert(message);
					}
				},

				reload: function () {

					models.load().done(function (collection) {

						var view = new module.TileCollectionView({
							collection: collection
						});

						view.on('childview:webui:tile:click', api.open);

						app.setContentView(view);
					});
				}
			};

			module.start = function () {
				api.reload();
			};
		});

		return application.WebUI.Tiles;
	});