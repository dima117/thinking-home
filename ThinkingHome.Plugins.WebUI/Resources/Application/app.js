define(['marionette', 'backbone'], function (Marionette, Backbone) {

	var app = new Marionette.Application();

	app.addRegions({
		regionMenu: "#region-menu",
		regionMenuRight: "#region-right-menu",
		regionContent: "#region-page-content"
	});
	
	app.navigate = function (route, options) {

		options || (options = {});

		Backbone.history.navigate(route, options);
	};
	
	app.setContentView = function(view) {
		app.regionContent.show(view);
	},

	app.on('initialize:after', function() {

		if (Backbone.history) {
			Backbone.history.start();
		}
	});

	return app;
});