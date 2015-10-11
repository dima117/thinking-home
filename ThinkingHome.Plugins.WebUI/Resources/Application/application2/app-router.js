define(['lib'], function (lib) {

	var router = lib.backbone.Router.extend({

		routes: {
			'*path': '_processRoute'
		},

		setPath: function (route, args) {
			var path = route + this._formatQueryString(args);
			lib.backbone.history.navigate(path);
		},

		start: function () {
			lib.backbone.history.start();
		},

		_processRoute: function (route, queryString) {
			var args = this._parseQueryString(queryString);
			this.trigger('navigate', route, args);
		},

		_parseQueryString: function (queryString) {

			if (lib._.isString(queryString)) {
				return queryString.split('/').map(function (arg) {
					return decodeURIComponent(arg);
				});
			}

			return [];
		},

		_formatQueryString: function (args) {

			if (lib._.isArray(args)) {

				return '?' + args.map(function (arg) {
					return encodeURIComponent(arg);
				}).join('/');
			}

			return '';
		},
	});

	return {
		Router: router
	};
});