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
		app.reqres.setHandler('load:tiles:editor-form', function () {
			return api.loadForm();
		});

		app.reqres.setHandler('load:tiles:editor-list', function () {
			return api.loadList();
		});
	});

	return application.WebUI.TilesEditor;
});