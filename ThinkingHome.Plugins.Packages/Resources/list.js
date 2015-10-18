define(
	['lib', 'webapp/packages/list-model', 'webapp/packages/list-view'],
	function (lib, models, views) {

		var packageList = lib.common.AppSection.extend({
			start: function () {
				this.reload();
			},

			install: function (childView) {
				var packageId = childView.model.get('id');
				models.installPackage(packageId).done(this.bind('reload'));
			},

			uninstall: function (childView) {
				var packageId = childView.model.get('id');
				models.uninstallPackage(packageId).done(this.bind('reload'));
			},

			displayList: function (items) {
				var view = new views.PackageListView({ collection: items });

				this.listenTo(view, 'childview:packages:install', this.bind('install'));
				this.listenTo(view, 'childview:packages:uninstall', this.bind('uninstall'));

				this.application.setContentView(view);
			},

			reload: function () {
				models.loadPackages().done(this.bind('displayList'));
			}
		});

		return packageList;
	});