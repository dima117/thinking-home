requirejs.config({
	baseUrl: 'js',
	paths: {
		tpl: 'vendor/tpl.min',
		json2: 'vendor/json2.min',
		jquery: 'vendor/jquery.min',
		underscore: 'vendor/underscore.min',
		backbone: 'vendor/backbone.min',
		marionette: 'vendor/backbone.marionette.min',
		bootstrap: 'vendor/bootstrap.min',
		
		navigation: 'application/navigation'
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

require(['app', 'navigation', 'bootstrap'], function (app) {
	app.start();
});