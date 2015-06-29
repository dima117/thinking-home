define([
	'lib',
	'text!application/dashboard.tpl'],
	function (lib, dashboardTemplate) {

		var dashboardView = lib.marionette.ItemView.extend({
			template: lib._.template(dashboardTemplate)
		});

		return {
			DashboardView: dashboardView
		};
	});