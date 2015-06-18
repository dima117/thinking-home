define(['lib'], function (lib) {

	var router = lib.marionette.Object.extend({

		// options
		defaultRoute: '',

		parseQueryString: function (queryString) {

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

		formatQueryString: function (args) {

			if (args && args.length) {

				var encoded = [];

				for (var i = 0; i < args.length; i++) {

					encoded.push(encodeURIComponent(args[i]));
				}

				return '?' + encoded.join('/');
			}

			return '';
		},

		navigate: function (route, args) {

			var self = this;

			if (route) {

				require([route], function (obj) {

					obj.start.apply(obj, args);

					route += self.formatQueryString(args);

					lib.backbone.history.navigate(route);
				});
			}

		},

		initialize: function (options) {

			var self = this;

			this.options = options;

			this.router = new lib.marionette.AppRouter({
				appRoutes: { '*path': 'loadPage' },
				controller: {
					loadPage: function (route, queryString) {

						var args = self.parseQueryString(queryString);
						self.navigate(route, args);
					}
				}
			});
		},

		start: function () {

			if (lib.backbone.history) {
				lib.backbone.history.start();

				if (lib.backbone.history.fragment === '') {

					var defaultRoute = this.getOption('defaultRoute');

					if (defaultRoute) {

						this.navigate(defaultRoute);
					}
				}
			}
		}
	});

	return router;
});