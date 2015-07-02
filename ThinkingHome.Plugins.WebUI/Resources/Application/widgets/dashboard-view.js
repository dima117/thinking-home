define([
	'lib',
	'text!application/dashboard-layout.tpl'],
	function (lib, layoutTemplate) {

		var navItemView = lib.marionette.ItemView.extend({
			template: lib._.template('<a href="#"><%=title%></a>'),
			tagName: 'li'
		});

		var navPanelView = lib.marionette.CollectionView.extend({
			childView: navItemView,
			tagName: 'ul',
			className: 'nav nav-menu'
		});

		var layoutView = lib.marionette.LayoutView.extend({
			template: lib._.template(layoutTemplate),
			regions: {
				nav: ".js-nav",
				content: ".js-content"
			}
		});

		return {
			NavPanelView: navPanelView,
			LayoutView: layoutView
		};
	});