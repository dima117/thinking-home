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
	});

	return application.WebUI.Tiles;
});