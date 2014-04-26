define(
	['app', 'tpl!webapp/packages/list.tpl', 'tpl!webapp/packages/list-item.tpl'],
	function (application, template, itemTemplate) {

		application.module('Packages.List', function (module, app, backbone, marionette, $, _) {

			module.PackageView = marionette.ItemView.extend({
				template: itemTemplate
			});

			module.PackageListView = marionette.CompositeView.extend({
				template: template,
				itemView: module.PackageView,
				itemViewContainer: '.ph-items'
			});
		});

		return application.Packages.List;
	});