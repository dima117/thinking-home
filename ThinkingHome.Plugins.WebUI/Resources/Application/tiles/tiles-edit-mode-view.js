define(
	['lib', 'common',
		'text!application/tiles/tile-edit-mode.tpl',
		'text!application/tiles/tiles-edit-mode.tpl'],
		function (lib, common, itemTemplate, listTemplate) {

			var tileViewEditMode = common.SortableItemView.extend({
				template: lib._.template(itemTemplate),
				tagName: 'a',
				className: 'th-tile btn-primary',
				onRender: function () {

					common.SortableItemView.prototype.onRender.call(this);

					if (this.model.get('wide')) {

						this.$el.addClass('th-tile-double');
					}
				},
				triggers: {
					'click .js-tile-delete': 'webui:tile:delete'
				}
			});

			// Collection View
			var tileCollectionViewEditMode = common.SortableCollectionView.extend({
				template: lib._.template(listTemplate),
				childView: tileViewEditMode,
				childViewContainer: '.js-list',
				onDropItem: function () {
					this.trigger('webui:tile:sort');
				}
			});
			
			return {
				TileViewEditMode: tileViewEditMode,
				TileCollectionViewEditMode: tileCollectionViewEditMode
			};
		});