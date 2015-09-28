define(['jquery'], function ($) {

	var api = {
		extendOptions: function (options) {
			var opts = options = $.extend({
				itemSelector: 'li',
				width: 100,
				height: 100,
				margin: 10
			}, options);

			opts.fw = options.width + 2 * options.margin;
			opts.fh = options.height + 2 * options.margin;
			return opts;
		},

		debounce: function (fn, delay) {
			var timeout;
			return function () {
				var args = arguments;

				if (timeout) {
					clearTimeout(timeout);
				}

				timeout = setTimeout(function () { fn(args); }, delay);
			}
		},
		colCount: function ($el, fw) {
			var cols = Math.floor($el.width() / fw);
			return cols < 1 ? 1 : cols;
		},
		reflow: function (items, cols, opts) {
			for (var i = 0; i < items.length; i++) {
				api.place(items.eq(i), i % cols, Math.floor(i / cols), opts);
			}
		},
		place: function ($el, x, y, opts) {
			$el.css({
				left: x * opts.fw + opts.margin,
				top: y * opts.fh + opts.margin
			});
		}
	};

	return $.fn.dashboard = function (options) {
		var opts = api.extendOptions(options),
			self = this,
			items = self.find(opts.itemSelector),
			cols = api.colCount(self, opts.fw);

		api.reflow(items, cols, opts);

		$(window).resize(api.debounce(function () {
			var cols2 = api.colCount(self, opts.fw);

			if (cols !== cols2) {
				self.trigger('cols:change', { prev: cols, next: cols2 });
				cols = cols2;
			}
		}, 350));

		self.addClass('x-dashboard')
			.on('cols:change', function (e, data) {
				api.reflow(items, data.next, opts);
			});

		items.addClass('x-dashboard-item')
			.css({
				width: opts.width,
				height: opts.height
			});
	};

});