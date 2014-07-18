define(
	['app', 'common',
		'text!application/tiles/tile-edit-mode.tpl',
		'text!application/tiles/tiles-edit-mode.tpl'],
		function (application, commonModule, itemTemplate, listTemplate) {

			application.module('WebUI.TilesEditMode', function (module, app, backbone, marionette, $, _) {

				module.TileViewEditMode = commonModule.SortableItemView.extend({
					template: _.template(itemTemplate),
					tagName: 'a',
					className: 'tile btn-primary',
					onRender: function () {

						commonModule.SortableItemView.prototype.onRender.call(this);

						if (this.model.get('wide')) {
							this.$el.addClass('tile-double');
						}
					},
					triggers: {
						'click .js-tile-delete': 'webui:tile:delete'
					}
				});

				// Collection View
				module.TileCollectionViewEditMode = commonModule.SortableCollectionView.extend({
					template: _.template(listTemplate),
					itemView: module.TileViewEditMode,
					itemViewContainer: '.js-list'
				});
			});

			return application.WebUI.TilesEditMode;
		});