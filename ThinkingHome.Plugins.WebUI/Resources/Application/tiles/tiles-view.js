define(
	[	'app', 'common',
		'text!application/tiles/tile.tpl',
		'text!application/tiles/tile-edit-mode.tpl'],
		function (application, commonModule, template, templateEditMode) {

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
						'click': 'webui:tile:click'
					}
				});
				
				module.TileViewEditMode = commonModule.SortableItemView.extend({
					template: _.template(templateEditMode),
					tagName: 'a',
					className: 'tile btn-info',
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
					itemView: module.TileViewEditMode,
					className: 'tiles'
				});

				module.TileCollectionView = marionette.CollectionView.extend({
					itemView: module.TileView,
					className: 'tiles'
				});
			});

			return application.WebUI.Tiles;
		});