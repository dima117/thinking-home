define(
	['lib', 'text!application/layout/app-layout.tpl', 'text!application/layout/app-menu.tpl'],
	function (lib, layoutTemplate, menuTemplate) {

		//var createLinkHandler = function (route, hideMenu) {
		//	return function (e) {
		//		e.preventDefault();
		//		e.stopPropagation();

		//		hideMenu && this.ui.navbarCollapse.collapse('hide');

		//		this.trigger('navigate', route);
		//	}
		//};

		var menuItemView = lib.marionette.ItemView.extend({
			template: lib.handlebars.compile('<a href="#">{{title}}</a>'),
			tagName: 'li',
			className: 'visible-xs-block',
			onRender: function () {
				var isActive = this.model.get('active');
				this.$el.toggleClass('active', isActive);
			}
		});

		var menuView = lib.marionette.CompositeView.extend({
			template: lib.handlebars.compile(menuTemplate),
			childView: menuItemView,
			tagName: 'ul',
			className: 'nav navbar-nav navbar-right'
		});

		var layoutView = lib.marionette.LayoutView.extend({
			el: 'body',
			template: lib.handlebars.compile(layoutTemplate),
			ui: {
				info: '.js-info',
				navbarCollapse: '.js-navbar-collapse'
			},
			regions: {
				content: ".js-content",
				menu: ".js-menu"
			},
			//events: {
			//	"click .js-link-home": createLinkHandler(undefined, true),
			//	"click .js-link-apps": createLinkHandler('apps', true),
			//	"click .js-link-settings": createLinkHandler('settings', true)
			//},

			// api
			setInfoText: function (text) {
				this.ui.info.text(text);
			}
		});

		return {
			MenuView: menuView,
			LayoutView: layoutView
		};
	});