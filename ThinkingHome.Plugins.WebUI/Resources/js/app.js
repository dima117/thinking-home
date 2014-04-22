define(['marionette', 'backbone'], function (Marionette, Backbone) {

	var app = new Marionette.Application();

	app.addRegions({
		navRegion: "#nav-region",
		mainRegion: "#main-region"
	});

	app.on('initialize:after', function() {

		if (Backbone.history) {
			Backbone.history.start();
		}
	});

	return app;
});