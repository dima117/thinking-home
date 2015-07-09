define([
	'lib',
	'text!application/dashboard-nothing.tpl',
	'text!application/dashboard-layout.tpl',
	'text!application/dashboard-widget.tpl'],
	function (lib, emptyTemplate, layoutTemplate, widgetTemplate) {

		var emptyView = lib.marionette.ItemView.extend({
			template: lib._.template(emptyTemplate)
		});

		var layoutView = lib.marionette.LayoutView.extend({
			template: lib.handlebars.compile(layoutTemplate),
			regions: {
				menu: '.js-menu',
				content: '.js-content'
			}
		});

		var menuItemView = lib.marionette.ItemView.extend({
			template: lib._.template('<a href="#"><%=title%></a>'),
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

		var widgetView = lib.marionette.ItemView.extend({
			template: lib._.template(widgetTemplate),
			className: 'row'
		});

		return {
			EmptyView: emptyView,
			LayoutView: layoutView,
			MenuView: menuView,
			WidgetView: widgetView
		};
	});