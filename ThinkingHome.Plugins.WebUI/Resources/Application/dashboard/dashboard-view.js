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
			className: 'th-dashboard-container',
			onShow: function () {
				this.$el.dashboard({
					itemSelector: '.js-panel',
					width: 320,
					height: 420
				});
			}
		});

		return {
			EmptyView: emptyView,
			LayoutView: layoutView
		};
	});