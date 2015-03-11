define(['lib'], function (lib) {

	// entities
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

		del: function (id) {

			return lib.$.post('/api/webui/tiles/delete', { id: id }).promise();
		},

		sort: function (collection) {

			var ids = [];

			collection.each(function (el) {
				var id = el.get('id');
				ids.push(id);
			});

			var json = JSON.stringify(ids);

			return lib.$.post('/api/webui/tiles/sort', { data: json }).promise();
		}
	};

	return {

		// entities
		TileModel: tileModel,
		TileCollection: tileCollection,

		// requests
		load: api.load,
		remove: api.del,
		saveOrder: api.sort
	};
});