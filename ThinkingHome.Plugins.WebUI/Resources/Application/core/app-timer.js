define(['lib'], function (lib) {
	var timer = lib.marionette.Object.extend({
		start: function () {
			this._handler();
			this._interval = setInterval(lib._.bind(this._handler, this), 2000);
		},
		onBeforeDestroy: function () {
			if (this._interval) {
				clearInterval(this._interval);
				this._interval = undefined;
			}
		},
		_handler: function () {
			this.trigger('update');
		}
	});

	return timer;
});