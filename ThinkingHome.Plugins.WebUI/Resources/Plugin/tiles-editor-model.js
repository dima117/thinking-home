define(['app'], function (application) {

	application.module('WebUI.TilesEditor', function (module, app, backbone, marionette, $, _) {

		module.TilesEditor = backbone.Model.extend();
		
		module.TilesEditorItem = backbone.Model.extend();

		module.TilesEditorItemCollection = backbone.Collection.extend({
			model: module.TilesEditorItem
		});

		var api = {
			loadForm: function () {

				var defer = $.Deferred();

				$.getJSON('/api/webui/tiles/editor')
					.done(function (obj) {

						var model = new module.TilesEditor(obj);

						var list = new module.TileCollection(obj.list);
						model.set('list', list);
						
						defer.resolve(model);
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

	return application.WebUI.TilesEditor;
});