define(['marionette', 'backbone'], function (marionette, backbone) {

	var app = new marionette.Application();

	app.addRegions({
		regionContent: "#region-page-content"
	});

	app.setContentView = function(view) {
		app.regionContent.show(view);
	};


	app.addTile = function (def, options) {

		var optionsJson = JSON.stringify(options);

		$.post('/api/webui/tiles/add', { def: def, options: optionsJson })
			.done(function () {
				app.navigate('tiles');
			});
	};

	app.navigate = function (route) {

		if (route) {

			var args = Array.prototype.slice.call(arguments, 1);

			require([route], function (obj) {

				obj.start.apply(obj, args);

				if (args && args.length) {
					//encodeURIComponent('edg/fwegw/e&wegewg/we/g')
					route += '?' + args.join('/');
				}

				backbone.history.navigate(route);
			});
		}
	};

	app.router = new marionette.AppRouter({
		appRoutes: { '*path(?*args)': 'loadPage' },
		controller: {
			loadPage: function (route, args) {

				//decodeURIComponent("edg%2Ffwegw%2Fe%26wegewg%2Fwe%2Fg");
				var x = args === null || args === undefined
					? [route]
					: [route].concat(args.split('/'));;

				app.navigate.apply(this, x);
			}
		}
	});
	
	app.on('page:load', app.navigate);

	app.on('initialize:after', function () {

		if (backbone.history) {
			backbone.history.start();

			if (Backbone.history.fragment === '') {
				app.navigate('tiles');
			}
		}
	});

	return app;
});