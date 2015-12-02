define(
	['lib', 'application/layout/layout-view'],
	function (lib, views) {

		var layout = lib.common.ApplicationBlock.extend({
			_linkHandler: function (route) {
				this.layout.hideNavbarMenu();
				this.trigger('navigate', route || '');
			},
			_customLinkHandler: function (view) {
				var route = view.model.get('route') || '',
					args = view.model.get('args') || [];

				this.layout.hideNavbarMenu();
				this.trigger('navigate', route, args);
			},

			initialize: function () {
				this.menuItems = new lib.common.MenuItemCollection();
				this.menu = new views.MenuView({ collection: this.menuItems });
				this.sideMenu = new views.SideMenuView({ collection: this.menuItems });

				this.layout = new views.LayoutView();

				// events
				this.listenTo(this.layout, 'navigate:home', this.bind('_linkHandler', ''));
				this.listenTo(this.menu, 'navigate:apps', this.bind('_linkHandler', 'apps'));
				this.listenTo(this.menu, 'navigate:settings', this.bind('_linkHandler', 'settings'));
				this.listenTo(this.menu, 'childview:navigate', this.bind('_customLinkHandler'));
				this.listenTo(this.sideMenu, 'childview:navigate', this.bind('_customLinkHandler'));
			},
			render: function () {
				this.layout.render();
				this.layout.showChildView('menu', this.menu);
				this.layout.showChildView('side', this.sideMenu);
			},
			onBeforeDestroy: function () {
				this.layout.destroy();
			},

			// api
			setContentView: function (view, menuItems) {
				var items = menuItems instanceof lib.common.MenuItemCollection ? menuItems.toJSON() : [];

				this.layout.showChildView('content', view);
				this.menuItems.reset(items);
			},

			setInfoText: function (text) {
				this.layout.setInfoText(text);
			}
		});

		return layout;
	});