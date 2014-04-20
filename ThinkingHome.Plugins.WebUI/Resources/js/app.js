define(['marionette'], function (Marionette) {

	var app = new Marionette.Application();

	app.on('initialize:after', function() {

		console.log('хрюката уже здесь!');
	});

	return app;
});