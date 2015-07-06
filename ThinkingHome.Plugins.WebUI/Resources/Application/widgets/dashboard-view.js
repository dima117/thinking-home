define([
	'lib',
	'text!application/dashboard-layout.tpl'],
	function (lib, layoutTemplate) {

		var navItemView = lib.marionette.ItemView.extend({
			template: lib._.template('<a href="#"><%=title%></a>'),
			tagName: 'li',
			initialize: function () {

				this.listenTo(this.model, "change:active", this.updateState);
			},
			updateState: function() {

				var isActive = this.model.get("active");
				this.$el.toggleClass("active", isActive);
			},
			triggers: {
				"click a": "nav:select"
			}
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