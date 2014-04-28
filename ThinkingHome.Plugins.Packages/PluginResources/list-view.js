define(
	['app', 'tpl!webapp/packages/list.tpl', 'tpl!webapp/packages/list-item.tpl'],
	function (application, template, itemTemplate) {

		application.module('Packages.List', function (module, app, backbone, marionette, $, _) {

			module.PackageView = marionette.ItemView.extend({
				template: itemTemplate,
				events: {
					'click .js-btn-install': 'btnInstallClick',
					'click .js-btn-uninstall': 'btnUninstallClick'
				},
				onRender: function () {

					var packageVersion = this.model.get('installedVersion');

					if (packageVersion) {
						this.$('.js-btn-install').hide();
					} else {
						this.$('.js-btn-uninstall').hide();
					}
				},
				btnInstallClick: function (e) {
					e.preventDefault();
					this.trigger('packages:install');
				},
				btnUninstallClick: function (e) {
					e.preventDefault();
					this.trigger('packages:uninstall');
				}
			});

			module.PackageListView = marionette.CompositeView.extend({
				template: template,
				itemView: module.PackageView,
				itemViewContainer: '.ph-items'
			});
		});

		return application.Packages.List;
	});