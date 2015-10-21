define(['marionette', 'underscore'], function (marionette, _) {

	var api = {
		bind: function (fn) {
			var func = _.isString(fn) ? this[fn] : fn,
				args = [].slice.call(arguments, 1),
				ctx = this;

			return function () {
				return func.apply(ctx, args.concat([].slice.call(arguments)));
			};
		}
	};

	var appSection = marionette.Object.extend({
		initialize: function (options) {
			this.application = options.application;
		},
		start: function () { },
		bind: api.bind
	});

	var widget = marionette.Object.extend({
		initialize: function (options) {
			this.application = options.application;
			this.region = options.region;
		},
		show: function (model) { },
		bind: api.bind
	});

	return {
		AppSection: appSection,
		Widget: widget
	};
});