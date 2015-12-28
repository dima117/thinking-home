requirejs.config({
	baseUrl: '/',
	paths: {
		lib: 'application/lib',

		text: 'vendor/js/require-text',
		json: 'vendor/js/require-json',
		lang: 'vendor/js/require-lang',

		json2: 'vendor/js/json2.min',
		jquery: 'vendor/js/jquery.min',
		underscore: 'vendor/js/underscore.min',
		backbone: 'vendor/js/backbone.min',
		marionette: 'vendor/js/backbone.marionette.min',
		syphon: 'vendor/js/backbone.syphon',
		bootstrap: 'vendor/js/bootstrap.min',
		moment: 'vendor/js/moment.min',
		codemirror: 'vendor/js/codemirror-all',
		handlebars: 'vendor/js/handlebars.amd.min',
		chart: 'vendor/js/chart.min',
		'chart.scatter': 'vendor/js/chart.scatter.min',
		'dashboard-layout': 'application/libs/dashboard-layout',
		'i18n': 'application/libs/i18n',
		'common': 'application/libs/common',

		signalr: 'vendor/js/jquery.signalr.min',

		dashboard:			'application/dashboard',
		apps:				'application/sections/user',
		settings:			'application/sections/system'
	},
	shim: {
		bootstrap: ['jquery'],
		backbone: {
			deps: ['json2', 'jquery', 'underscore'],
			exports: 'Backbone'
		},
		marionette: {
			deps: ['backbone', 'syphon'],
			exports: 'Marionette'
		},
		signalr: ['jquery'],
		json2: {
			exports: 'JSON'
		},
		syphon: {
			deps: ['backbone'],
			exports: 'Backbone.Syphon'
		}
	},
	map: {
		'chart.scatter': {
			'Chart': 'chart'
		}
	}
});

require(['application/core/app'], function (application) {
	window.app = new application();
	window.app.start();
});