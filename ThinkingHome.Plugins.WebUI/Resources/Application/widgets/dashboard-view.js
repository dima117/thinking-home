define([
	'lib',
	'text!webapp/uniui/ui/dashboard.tpl'],
	function (lib, dashboardTemplate) {

		var dashboardView = lib.marionette.ItemView.extend({
			template: lib._.template(dashboardTemplate)
		});

		return {
			DashboardView: dashboardView
		};
	});