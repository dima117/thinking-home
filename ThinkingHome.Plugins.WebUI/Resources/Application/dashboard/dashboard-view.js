define([
	'lib',
	'text!application/dashboard-nothing.tpl',
	'text!application/dashboard-layout.tpl',
	'lang!application/lang.json'],
	function (lib, emptyTemplate, layoutTemplate, lang) {

		var emptyView = lib.marionette.ItemView.extend({
			template: lib.handlebars.compile(emptyTemplate),
			templateHelpers: { lang: lang }
		});

		var layoutView = lib.marionette.LayoutView.extend({
			template: lib.handlebars.compile(layoutTemplate),
			className: 'th-dashboard-container',
			templateHelpers: { lang: lang },
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