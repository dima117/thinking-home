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

	var api = {
		parseParameters: function(queryString) {
			
			var result = [];

			if (queryString !== null && queryString !== undefined) {

				var params = (queryString + '').split('/');

				for (var i = 0; i < params.length; i++) {

					var decodedValue = decodeURIComponent(params[i]);
					result.push(decodedValue);
				}
			}

			return result;
		},

		loadRoute: function(route, args) {
			
			require([route], function (obj) {

				obj.start.apply(obj, args);

				if (args && args.length) {

					var encoded = [];

					for (var i = 0; i < args.length; i++) {
						encoded.push(encodeURIComponent(args[i]));
					}

					route += '?' + encoded.join('/');
				}

				backbone.history.navigate(route);
			});
		}
	};

	app.navigate = function (route) {
	
		if (route) {

			var args = Array.prototype.slice.call(arguments, 1);
			api.loadRoute.call(this, route, args);
		}
	};

	app.router = new marionette.AppRouter({
		appRoutes: { '*path': 'loadPage' },
		controller: {
			loadPage: function (route, queryString) {

				var args = api.parseParameters(queryString);
				api.loadRoute.call(this, route, args);
			}
		}
	});
	
	app.on('page:load', app.navigate);

	app.on('start', function () {

		if (backbone.history) {
			backbone.history.start();

			if (Backbone.history.fragment === '') {
				app.navigate('tiles');
			}
		}
	});

	return app;
});