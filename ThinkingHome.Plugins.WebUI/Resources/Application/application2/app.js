define(
	['lib', 'application2/app-view', 'application2/app-router'],
	function (lib, views, routing) {

		var homeApplication = lib.marionette.Application.extend({
			initialize: function (options) {
				this.layout = new views.LayoutView();
				this.layout.on('navigate', this._loadPage, this);

				this.router = new routing.Router();
				this.router.on('navigate', this._loadPage, this);
			},
			onStart: function () {
				this.layout.render();
				this.router.start();
			},
			onBeforeDestroy: function () {
				this.layout.destroy();
			},

			// shortcuts
			setContentView: function (view) {
				this.layout.setContentView(view);
			},

			navigate: function (route) {
				var args = Array.prototype.slice.call(arguments, 1);
				this._loadPage(route, args);
			},

			// private
			_loadPage: function (route, args) {

				var self = this;

				route = route || 'dashboard';
				args = args || [];

				require([route], function (obj) {

					obj.start.apply(obj, args);
					self.router.setPath(route, args);
				});
			}
		});

		return homeApplication;
	});