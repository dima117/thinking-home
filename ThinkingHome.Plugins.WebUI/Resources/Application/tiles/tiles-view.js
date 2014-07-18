define(
	[	'app', 'common',
		'text!application/tiles/tile.tpl',
		'text!application/tiles/tiles.tpl'],
		function (application, commonModule, itemTemplate, listTemplate) {

			application.module('WebUI.Tiles', function (module, app, backbone, marionette, $, _) {

				module.TileView = marionette.ItemView.extend({
					template: _.template(itemTemplate),
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
						'click': 'webui:tile:click'
					}
				});

				// Collection View
				module.TileCollectionView = marionette.CompositeView.extend({
					template: _.template(listTemplate),
					itemView: module.TileView,
					itemViewContainer: '.js-list'
				});
			});

			return application.WebUI.Tiles;
		});