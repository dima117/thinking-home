define(['app'], function (application) {

	application.module('WebUI.Tiles', function (module, app, backbone, marionette, $, _) {

		module.Tile = backbone.Model.extend({
			defaults: {
				id: null,
				title: null,
				wide: false,
				content: []
			}
		});

		module.TileCollection = backbone.Collection.extend({
			model: module.Tile
		});

		var api = {
			loadTiles: function () {

				var defer = $.Deferred();

				$.getJSON('/api/webui/tiles/all')
					.done(function (tiles) {
						var collection = new module.TileCollection(tiles);
						defer.resolve(collection);
					})
					.fail(function () {

						defer.resolve(undefined);
					});

				return defer.promise();
			}
		};
		
		// requests
		app.reqres.setHandler('load:tiles:all', function () {
			return api.loadTiles();
		});
	});

	return application.WebUI.Tiles;
});