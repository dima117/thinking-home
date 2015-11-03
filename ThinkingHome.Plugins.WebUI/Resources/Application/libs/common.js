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

	var applicationBlock = marionette.Object.extend({
		bind: api.bind
	});

	var appSection = applicationBlock.extend({
		initialize: function (options) {
			this.application = options.application;
		},
		start: function () { }
	});

	var widget = applicationBlock.extend({
		initialize: function (options) {
			this.application = options.application;
			this.region = options.region;
		},
		show: function (model) { }
	});

	return {
		ApplicationBlock: applicationBlock,
		AppSection: appSection,
		Widget: widget
	};
});