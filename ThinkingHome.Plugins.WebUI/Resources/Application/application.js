define(['lib'], function(lib, router, layout) {

	var app = new lib.marionette.Application();

	app.router = new router();
	app.layout = new layout();

	app.navigate = function(path) {
		
	};

	app.router.on('navigate', app.navigate);
	app.layout.on('navigate', app.navigate);

	app.on('start', function () {

		app.layout.render();
		app.router.start();
	});

	return app;
});