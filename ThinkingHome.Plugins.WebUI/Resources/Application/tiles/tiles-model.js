define(['lib'], function (lib) {

	var tileModel = lib.backbone.Model.extend({
		defaults: {
			id: null,
			title: null,
			wide: false,
			content: []
		}
	});

	var tileCollection = lib.backbone.Collection.extend({
		model: tileModel
	});

	var api = {
		load: function () {

			var defer = lib.$.Deferred();

			lib.$.getJSON('/api/webui/tiles')
				.done(function (tiles) {

					var collection = new tileCollection(tiles);
					defer.resolve(collection);
				})
				.fail(function () {

					defer.resolve(undefined);
				});

			return defer.promise();
		},

		action: function (id) {

			return lib.$.post('/api/webui/tiles/action', { id: id }).promise();
		}
	};

	return {

		// entities
		Tile: tileModel,
		TileCollection: tileCollection,

		// requests
		load: api.load,
		action: api.action
	};
});