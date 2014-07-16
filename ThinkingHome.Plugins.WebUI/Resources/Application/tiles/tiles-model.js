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
			load: function () {

				var defer = $.Deferred();

				$.getJSON('/api/webui/tiles')
					.done(function (tiles) {
						var collection = new module.TileCollection(tiles);
						defer.resolve(collection);
					})
					.fail(function () {

						defer.resolve(undefined);
					});

				return defer.promise();
			},
			
			del: function (id) {

				return $.post('/api/webui/tiles/delete', { id: id }).promise();
			},
			
			action: function (id) {

				return $.post('/api/webui/tiles/action', { id: id }).promise();
			}
		};
		
		// requests
		app.reqres.setHandler('load:tiles:all', api.load);
		app.reqres.setHandler('update:tiles:delete', api.del);
		app.reqres.setHandler('update:tiles:action', api.action);
	});

	return application.WebUI.Tiles;
});