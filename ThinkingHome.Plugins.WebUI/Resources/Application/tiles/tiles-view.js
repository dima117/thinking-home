define(
	['lib',
		'text!application/tiles/tile.tpl',
		'text!application/tiles/tiles.tpl'],
		function (lib, itemTemplate, listTemplate) {

			var tileView = lib.marionette.ItemView.extend({
				template: lib._.template(itemTemplate),
				tagName: 'a',
				className: 'th-tile',
				onRender: function () {

					var className = this.model.get('className') || "btn-primary";
					this.$el.addClass(className);

					if (this.model.get('wide')) {

						this.$el.addClass('th-tile-double');
					}
				},
				triggers: {
					'click': 'webui:tile:click'
				}
			});

			// Collection View
			var tileCollectionView = lib.marionette.CompositeView.extend({
				template: lib._.template(listTemplate),
				childView: tileView,
				childViewContainer: '.js-list'
			});


			return {
				
				TileView: tileView,
				TileCollectionView: tileCollectionView
			};
		});