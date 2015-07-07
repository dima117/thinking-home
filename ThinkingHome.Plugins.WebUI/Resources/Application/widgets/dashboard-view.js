define([
	'lib',
	'text!application/dashboard-layout.tpl',
	'text!application/dashboard-nothing.tpl'],
	function (lib, layoutTemplate, emptyTemplate) {



		var emptyView = lib.marionette.ItemView.extend({
			template: lib._.template(emptyTemplate)
		});

		return {
			EmptyView: emptyView
		};
	});