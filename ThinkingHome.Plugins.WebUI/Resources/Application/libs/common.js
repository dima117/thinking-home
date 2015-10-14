define(['marionette', 'underscore'], function (marionette, _) {

	var appSection = marionette.Object.extend({
		initialize: function (options) {
			if (!options.application)
				throw new Error('application is undefined');

			this.application = options.application;
		},
		start: function () {
		},
		onBeforeDestroy: function () {

		},
		bind: function (fn) {
			var func = _.isString(fn) ? this[fn] : fn,
				args = [].slice.call(arguments, 1);
				ctx = this;

			return function () {
				return func.apply(ctx, args.concat([].slice.call(arguments)));
			};
		}
	});

	return {
		AppSection: appSection
	};
});