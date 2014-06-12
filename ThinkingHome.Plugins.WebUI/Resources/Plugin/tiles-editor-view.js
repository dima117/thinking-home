define(['app', 'tpl!webapp/webui/tile.tpl'], function (application, template) {

	application.module('WebUI.TilesEditor', function (module, app, backbone, marionette, $, _) {

		module.TileView = marionette.ItemView.extend({
			template: template,
			tagName: 'a',
			className: 'tile btn-primary',
			onRender: function () {

				if (this.model.get('wide')) {
					this.$el.addClass('tile-double');
				}
			}
		});

		module.TileCollectionView = marionette.CollectionView.extend({
			itemView: module.TileView,
			className: 'tiles'
		});
	});

	return application.WebUI.TilesEditor;
});