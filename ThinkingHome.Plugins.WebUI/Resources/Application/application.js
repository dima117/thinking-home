define([
	'application2/app',
	'lib',
	'application/router',
	'application/layout',
	'json!api/webui/styles.json'],
	function (application2, lib, router, layout, cssFiles) {

		//var app2 = new application2();

		//return app2;


		// init
		var app = new lib.marionette.Application();

		app.router = new router({
			defaultRoute: 'dashboard'
		});

		app.layout = new layout({
			template: '#layout-template'
		});

		app.radio = new lib.marionette.Object();

		lib.$.connection.messageQueueHub.client.serverMessage = function (message) {
			app.radio.trigger(message.channel, message);
		};

		// start
		app.on('start', function () {

			lib.utils.loadCss.apply(null, cssFiles);

			this.layout.render();
			this.router.start();

			$.connection.hub.start()
				.done(function () { console.log('done'); })
				.fail(function () { console.log('fail', arguments); });
		});

		// shortcuts
		app.sendServerMessage = function (channel, data) {
			$.connection.messageQueueHub.invoke("Send", channel, data);
		};

		app.setContentView = function (view) {

			this.layout.setContentView(view);
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