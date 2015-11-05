define(
	['lib', 'application/layout/layout-model', 'application/layout/layout-view'],
	function (lib, models, views) {

		var layout = lib.common.ApplicationBlock.extend({
			_linkHandler: function (route) {
				this.layout.hideNavbarMenu();
				this.trigger('navigate', route || '');
			},

			initialize: function () {
				this.menuItems = new models.MenuItemCollection();
				this.menu = new views.MenuView({ collection: this.menuItems });

				this.layout = new views.LayoutView();

				// events
				this.listenTo(this.layout, 'navigate:home', this.bind('_linkHandler', ''));
				this.listenTo(this.menu, 'navigate:apps', this.bind('_linkHandler', 'apps'));
				this.listenTo(this.menu, 'navigate:settings', this.bind('_linkHandler', 'settings'));
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