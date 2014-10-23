requirejs.config({
	baseUrl: '/',
	paths: {
		app: 'application/app',
		common: 'application/common',

		text: 'vendor/js/require-text',
		json: 'vendor/js/require-json',

		json2: 'vendor/js/json2.min',
		jquery: 'vendor/js/jquery.min',
		underscore: 'vendor/js/underscore.min',
		backbone: 'vendor/js/backbone.min',
		marionette: 'vendor/js/backbone.marionette.min',
		syphon: 'vendor/js/backbone.syphon',
		bootstrap: 'vendor/js/bootstrap.min',
		moment: 'vendor/js/moment.min',
		codemirror: 'vendor/js/codemirror-all',
		highcharts: 'vendor/js/highcharts.min',
		
		tiles:				'application/tiles/tiles',
		'tiles-edit-mode':	'application/tiles/tiles-edit-mode',
		apps:				'application/sections/user',
		settings:			'application/sections/system'
	},
	shim: {
		bootstrap: ['jquery'],
		backbone: {
			deps: ['json2', 'jquery', 'underscore'],
			exports: 'Backbone'
		},
		syphon: {
			deps: ['backbone'],
			exports: 'Backbone.Syphon'
		},
		marionette: {
			deps: ['backbone', 'syphon'],
			exports: 'Marionette'
		},
		highcharts: {
			exports: 'Highcharts'
		}
	}
});

require(['app', 'common', 'bootstrap'], function (app) {
	app.start();
});