define(
	[	'app',
		'text!webapp/webui/tile.tpl'],
		function (application, template) {

			application.module('WebUI.Tiles', function (module, app, backbone, marionette, $, _) {

				module.TileView = marionette.ItemView.extend({
					template: _.template(template),
					tagName: 'a',
					className: 'tile',
					onRender: function () {

						var className = this.model.get('className') || "btn-primary";
						this.$el.addClass(className);

						if (this.model.get('wide')) {
							this.$el.addClass('tile-double');
						}
					},
					triggers: {
						'click': 'webui:tile:click',
						'click .js-tile-delete': 'webui:tile:delete'
					}
				});

				module.TileCollectionView = marionette.CollectionView.extend({
					itemView: module.TileView,
					className: 'tiles'
				});
			});

			return application.WebUI.Tiles;
		});