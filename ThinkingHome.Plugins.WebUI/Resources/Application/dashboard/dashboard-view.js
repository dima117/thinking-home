define([
	'lib',
	'text!application/dashboard-nothing.tpl',
	'text!application/dashboard-layout.tpl'],
	function (lib, emptyTemplate, layoutTemplate) {

		var emptyView = lib.marionette.ItemView.extend({
			template: lib.handlebars.compile(emptyTemplate)
		});

		var layoutView = lib.marionette.LayoutView.extend({
			template: lib.handlebars.compile(layoutTemplate),
			onShow: function () {
				this.$('.js-container').dashboard({
					itemSelector: '.js-panel',
					width: 320,
					height: 420
				});
			},
			regions: {
				menu: '.js-menu',
				content: '.js-content'
			}
		});

		var menuItemView = lib.marionette.ItemView.extend({
			template: lib.handlebars.compile('<a href="#">{{title}}</a>'),
			tagName: 'li',
			onRender: function () {
				var isActive = this.model.get("active");
				this.$el.toggleClass("active", isActive);
			},
			triggers: {
				"click a": "dashboard:select"
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