define(
	['lib', 'text!application/layout/app-layout.tpl', 'text!application/layout/app-menu.tpl'],
	function (lib, layoutTemplate, menuTemplate) {

		var sideMenuItemView = lib.marionette.ItemView.extend({
			template: lib.handlebars.compile('<a href="#" class="js-menu-link">{{title}}</a>'),
			tagName: 'li',
			onRender: function () {
				var isActive = this.model.get('active');
				this.$el.toggleClass('active', isActive);
			},
			triggers: {
				'click .js-menu-link': 'navigate'
			}
		});

		var menuItemView = sideMenuItemView.extend({
			className: 'visible-xs-block'
		});

		var menuView = lib.marionette.CompositeView.extend({
			template: lib.handlebars.compile(menuTemplate),
			childView: menuItemView,
			tagName: 'ul',
			className: 'nav navbar-nav navbar-right',
			triggers: {
				'click .js-link-apps': 'navigate:apps',
				'click .js-link-settings': 'navigate:settings'
			}
		});

		var sideMenuView = lib.marionette.CollectionView.extend({
			childView: sideMenuItemView,
			className: 'nav nav-menu',
			tagName: 'ul'
		});

		var layoutView = lib.marionette.LayoutView.extend({
			el: 'body',
			template: lib.handlebars.compile(layoutTemplate),
			ui: {
				info: '.js-info',
				container: '.js-container',
				navbarCollapse: '.js-navbar-collapse'
			},
			regions: {
				content: '.js-content',
				menu: '.js-menu',
				side: '.js-side-menu'
			},
			triggers: {
				'click .js-link-home': 'navigate:home'
			},

			// api
			setInfoText: function (text) {
				this.ui.info.text(text);
			},

			hideNavbarMenu: function() {
				this.ui.navbarCollapse.collapse('hide');
			},

			toggleSideMenu: function (isVisible) {
				this.ui.container.toggleClass('th-displayed-menu', !!isVisible);
			}
		});

		return {
			MenuView: menuView,
			SideMenuView: sideMenuView,
			LayoutView: layoutView
		};
	});