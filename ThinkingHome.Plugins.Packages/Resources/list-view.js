define(
	[	'app',
		'text!webapp/packages/list.tpl',
		'text!webapp/packages/list-item.tpl'],
	function (application, listTemplate, itemTemplate) {

		application.module('Packages.List', function (module, app, backbone, marionette, $, _) {

			module.PackageView = marionette.ItemView.extend({
				template: _.template(itemTemplate),
				triggers: {
					'click .js-btn-install': 'packages:install',
					'click .js-btn-uninstall': 'packages:uninstall'
				},
				onRender: function () {

					var packageVersion = this.model.get('installedVersion');

					if (packageVersion) {
						this.$('.js-btn-install').hide();
						this.$el.addClass('bg-success');
					} else {
						this.$('.js-btn-uninstall').hide();
					}
				}
			});

			module.PackageListView = marionette.CompositeView.extend({
				template: _.template(listTemplate),
				childView: module.PackageView,
				childViewContainer: '.js-list'
			});
		});

		return application.Packages.List;
	});