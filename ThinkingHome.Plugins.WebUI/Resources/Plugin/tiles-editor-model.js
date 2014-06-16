define(['app'], function (application) {

	application.module('WebUI.TilesEditor', function (module, app, backbone, marionette, $, _) {

		module.TilesEditor = backbone.Model.extend({
			defaults: { content: '' }
		});

		module.Tile = backbone.Model.extend();

		module.TileCollection = backbone.Collection.extend({
			model: module.Tile
		});

		var api = {

			addTile: function (key) {

				var rq = $.post('/api/tiles/editor-add', { key: key });

				return rq.promise();
			},

			loadForm: function () {

				var defer = $.Deferred();

				$.getJSON('/api/webui/tiles/editor-form')
					.done(function (obj) {

						var model = new module.TilesEditor(obj);
						defer.resolve(model);
					})
					.fail(function () {

						defer.resolve(undefined);
					});

				return defer.promise();
			},

			loadList: function () {

				var defer = $.Deferred();

				$.getJSON('/api/webui/tiles/editor-list')
					.done(function (items) {
						var collection = new module.TileCollection(items);
						defer.resolve(collection);
					})
					.fail(function () {
						defer.resolve(undefined);
					});

				return defer.promise();
			}
		};

		// requests
		app.reqres.setHandler('load:tiles:editor-form', api.loadForm);
		app.reqres.setHandler('load:tiles:editor-list', api.loadList);
		app.reqres.setHandler('update:tiles:editor-add', api.addTile);
	});

	return application.WebUI.TilesEditor;
});