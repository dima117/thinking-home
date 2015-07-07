define([
	'lib',
	'text!application/dashboard-nothing.tpl',
	'text!application/dashboard-layout.tpl'],
	function (lib, emptyTemplate, layoutTemplate) {

		var emptyView = lib.marionette.ItemView.extend({
			template: lib._.template(emptyTemplate)
		});

		var layoutView = lib.marionette.LayoutView.extend({
			template: lib._.template(layoutTemplate),
			regions: {
				menu: ".js-menu",
				content: ".js-content"
			}
		});

		var menuItemView = lib.marionette.ItemView.extend({
			template: lib._.template('<a href="#"><%=title%></a>'),
			tagName: 'li',
			onRender: function () {

				var isActive = this.model.get("active");
				this.$el.toggleClass("active", isActive);
			}
		});

		var menuView = lib.marionette.CollectionView.extend({
			childView: menuItemView,
			className: 'nav nav-menu',
			tagName: 'ul'
		});

		return {
			EmptyView: emptyView,
			LayoutView: layoutView,
			MenuView: menuView
		};
	});