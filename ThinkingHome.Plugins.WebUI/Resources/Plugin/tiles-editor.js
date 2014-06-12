define(['app', 'webapp/webui/tiles-editor-model', 'webapp/webui/tiles-editor-view'], function (application) {

	application.module('WebUI.TilesEditor', function (module, app, backbone, marionette, $, _) {

		var api = {

			reload: function () {

				app.request('load:tiles:all').done(function (collection) {
					
					var view = new module.TileCollectionView({
						collection: collection
					});
					
					app.setContentView(view);
				});
			}
		};

		module.start = function () {
			api.reload();
		};
	});

	return application.WebUI.TilesEditor;
});