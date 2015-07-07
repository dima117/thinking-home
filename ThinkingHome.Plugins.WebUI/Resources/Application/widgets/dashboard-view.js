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
				nav: ".js-menu",
				content: ".js-content"
			}
		});

		return {
			EmptyView: emptyView,
			LayoutView: layoutView
		};
	});