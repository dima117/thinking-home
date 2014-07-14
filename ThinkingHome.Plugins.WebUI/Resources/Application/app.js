define(['marionette', 'backbone'], function (marionette, backbone) {

	var app = new marionette.Application();

	app.addRegions({
		regionContent: "#region-page-content"
	});

	app.setContentView = function (view) {
		app.regionContent.show(view);
	},

	app.navigate = function (route) {

		if (route) {

			var args = Array.prototype.slice.call(arguments, 1);

			require([route], function (obj) {

				obj.start.apply(obj, args);
				backbone.history.navigate(route);
			});
		}
	};

	app.addTile = function (def, options) {

		var optionsJson = JSON.stringify(options);

		$.post('/api/webui/tiles/add', { def: def, options: optionsJson })
			.done(function () {
				app.navigate('tiles');
			});
	};

	app.router = new marionette.AppRouter({
		appRoutes: { '*path': 'loadPage' },
		controller: { loadPage: app.navigate }
	});

	app.on('page:load', app.navigate);

	app.on('initialize:after', function () {

		if (backbone.history) {
			backbone.history.start();

			if (Backbone.history.fragment === '') {
				app.navigate('tiles');
			}
		}
	});

	return app;
});