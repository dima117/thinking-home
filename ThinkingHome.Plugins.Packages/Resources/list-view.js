define(
	['lib',
		'text!webapp/packages/list.tpl',
		'text!webapp/packages/list-item.tpl',
		'lang!webapp/packages/lang.json'],
	function (lib, listTemplate, itemTemplate, lang) {

		var packageView = lib.marionette.ItemView.extend({
			template: lib.handlebars.compile(itemTemplate),
			triggers: {
				'click .js-btn-install': 'packages:install',
				'click .js-btn-uninstall': 'packages:uninstall'
			},
			onRender: function () {

				var packageVersion = this.model.get('installedVersion');

				if (packageVersion) {

					this.$('.js-btn-install').hide();
					this.$('.js-btn-uninstall').stateSwitcher();
				} else {

					this.$('.js-btn-install').stateSwitcher();
					this.$('.js-btn-uninstall').hide();
				}
			},
			templateHelpers: { lang: lang }
		});

		var packageListView = lib.marionette.CompositeView.extend({
			template: lib.handlebars.compile(listTemplate),
			childView: packageView,
			childViewContainer: '.js-list',
			templateHelpers: { lang: lang }
		});


		return {
			PackageView: packageView,
			PackageListView: packageListView
		};
	});