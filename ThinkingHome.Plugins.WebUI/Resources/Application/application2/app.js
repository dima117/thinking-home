define(
	['lib', 'application2/app-view'],
	function (lib, views) {

		var homeApplication = lib.marionette.Application.extend({
			initialize: function (options) {
			
				this.layout = new views.LayoutView()
			},
			onStart: function () {

				this.layout.render();
			},
			onBeforeDestroy: function () {
				
				this.layout.destroy();
			}
		});

		return homeApplication;
	});