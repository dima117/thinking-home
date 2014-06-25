define(
	[	'app',
		'text!webapp/packages/list.tpl',
		'text!webapp/packages/list-item.tpl'],
	function (application, listTemplate, itemTemplate) {

		application.module('Packages.List', function (module, app, backbone, marionette, $, _) {

			module.PackageView = marionette.ItemView.extend({
				template: _.template(itemTemplate),
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

			module.PackageListView = marionette.CompositeView.extend({
				template: _.template(listTemplate),
				itemView: module.PackageView,
				itemViewContainer: '.js-list'
			});
		});

		return application.Packages.List;
	});