define(
	['app', 'tpl!webapp/packages/layout.tpl', 'tpl!webapp/packages/list-item.tpl'],
	function (application, layoutTemplate, itemTemplate) {

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
						this.$el.addClass('bg-success');
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

			module.PackageListView = marionette.CollectionView.extend({
				 itemView: module.PackageView
			});

			module.PackageListLayout = marionette.Layout.extend({
				template: layoutTemplate,
				regions: {
					regionList: '.ph-list'
				}
			});
		});

		return application.Packages.List;
	});