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
		bindFnContext: function (fn, provideInnerContext) {
			var func = _.isString(fn) ? this[fn] : fn,
				ctx = this;

			return function () {
				var args = provideInnerContext ? [this].concat([].slice.call(arguments)) : arguments;
				return func.apply(ctx, args);
			};
		}
	});

	return {
		AppSection: appSection
	};
});