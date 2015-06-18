define(['lib', 'application/router', 'application/layout'], function (lib, router, layout) {

	var app = new lib.marionette.Application();

	app.router = new router({
		defaultRoute: 'tiles'
	});

	app.layout = new layout({
		template: '#layout-template',
		el: '#hrukata-container'
	});

	app.on('start', function () {

		app.layout.render();
		app.router.start();
	});






	app.addRegions({
		regionContent: "#region-page-content"
	});

	app.setContentView = function (view) {
		app.regionContent.show(view);
	};

	app.addTile = function (def, options) {

		var optionsJson = JSON.stringify(options);

		$.post('/api/webui/tiles/add', { def: def, options: optionsJson })
			.done(function () {
				app.router.navigate('tiles');
			});
	};

	app.navigate = function (route) {

		var args = Array.prototype.slice.call(arguments, 1);
		app.router.navigate(route, args);
	};

	app.loadPath = function (route, args) {

		app.router.navigate(route, args);
	};

	return app;
});