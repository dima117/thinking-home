requirejs.config({
	baseUrl: '/',
	paths: {
		app: 'application/app',
		common: 'application/common/common',
		css: 'application/style-loader',

		text: 'vendor/js/require-text',
		json: 'vendor/js/require-json',

		json2: 'vendor/js/json2.min',
		jquery: 'vendor/js/jquery.min',
		underscore: 'vendor/js/underscore.min',
		backbone: 'vendor/js/backbone.min',
		marionette: 'vendor/js/backbone.marionette.min',
		syphon: 'vendor/js/backbone.syphon',
		bootstrap: 'vendor/js/bootstrap.min',
		codemirror: 'vendor/js/codemirror-all',
		
		tiles: 'webapp/webui/tiles',
		'tiles-editor': 'webapp/webui/tiles-editor',
		apps: 'webapp/webui/section-list-common',
		settings: 'webapp/webui/section-list-system'
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
		}
	}
});

require(['app', 'common', 'bootstrap', 'css'], function (app) {
	app.start();
});