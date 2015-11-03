define(
	['lib', 'application/layout/layout-model', 'application/layout/layout-view'],
	function (lib, models, views) {

		var layout = lib.common.ApplicationBlock.extend({
			initialize: function () {
				this.menuItems = new models.MenuItemCollection();
				this.menu = new views.MenuView({ collection: this.menuItems });

				this.layout = new views.LayoutView();
			},
			render: function () {
				this.layout.render();
				this.layout.showChildView('menu', this.menu);
			},
			onBeforeDestroy: function () {
				this.layout.destroy();
			},

			// api
			setContentView: function (view, menuItems) {
				var items = menuItems ? menuItems.toJSON() : [];

				this.layout.showChildView('content', view);
				this.menuItems.reset(items);
			},

			setInfoText: function (text) {
				this.layout.setInfoText(text);
			}
		});

		return layout;
	});