define([
	'lib',
	'text!webapp/uniui/settings/dashboard-info.tpl',
	'text!webapp/uniui/settings/dashboard-info-widget.tpl'],
	function (lib, infoTemplate, widgetTemplate) {

		var widgetView = lib.marionette.ItemView.extend({
			template: lib._.template(widgetTemplate)
		});

		var infoView = lib.marionette.CompositeView.extend({
			template: lib._.template(infoTemplate),
			childView: widgetView,
			childViewContainer: '.js-list',
		});

		return {
			DashboardInfoView: infoView
		};
	});