requirejs.config({
	baseUrl: 'js',
	paths: {
		json2: 'vendor/json2.min',
		jquery: 'vendor/jquery.min',
		underscore: 'vendor/underscore.min',
		backbone: 'vendor/backbone.min',
		marionette: 'vendor/backbone.marionette.min',
		bootstrap: 'vendor/bootstrap.min'
	},
	shim: {
		bootstrap: ['jquery'],
		backbone: {
			deps: ['json2', 'jquery', 'underscore'],
			exports: 'Backbone'
		},
		marionette: {
			deps: ['backbone'],
			exports: 'Marionette'
		}
	}
});

require(['bootstrap'], function() {
});

require(['app'], function (app) {
	app.start();
});