define(
	[
		'marionette',
		'backbone',
		'underscore',
		'jquery',
		'json2',
		'syphon',
		'handlebars',
		'moment',
		'chart',
		'chart.scatter',
		'bootstrap',
		'dashboard-layout',
		'signalr',
		'hubs'
	],
	function (marionette, backbone, underscore, jquery, json2, syphon, handlebars, moment, chartjs) {

		var utils = {
			formatString: function () {

				var s = arguments[0];
				for (var i = 0; i < arguments.length - 1; i++) {
					var reg = new RegExp("\\{" + i + "\\}", "gm");
					s = s.replace(reg, arguments[i + 1]);
				}

				return s;
			},

			alert: function () {
				var msg = utils.formatString.apply(null, arguments);
				window.alert(msg);
			},

			confirm: function () {
				var msg = utils.formatString.apply(null, arguments);
				return window.confirm(msg);
			},

			loadCss: function () {

				for (var i = 0; i < arguments.length; i++) {

					jquery('<link type="text/css" rel="stylesheet" />')
						.attr('href', arguments[i])
						.appendTo("head");
				}
			},

			displayCurrentTime: function (selector) {

				var $el = jquery(selector);
				var fn = function () {

					$el.text(moment().format('LT, ll'));
				};

				if ($el.length) {

					fn();
					return window.setInterval(fn, 2000);
				}

				return undefined;
			},

			addListItems: function (select, items, options) {

				var opt = underscore.extend({
					text: 'name',
					value: 'id'
				}, options);

				underscore.each(items, function (item) {

					var val = item[opt.value],
						text = item[opt.text];

					jquery('<option />').val(val).text(text).appendTo(select);
				});
			}
		};

		// switchers
		jquery.fn.stateSwitcher = function () {

			var onEnter = function () {

				var $el = jquery(this),
					data = $el.data("state-switcher-params");

				$el.removeClass(data.stateClass)
				.addClass(data.actionClass)
				.text(data.actionText);
			},
			onLeave = function () {

				var $el = jquery(this),
					data = $el.data("state-switcher-params");

				$el.removeClass(data.actionClass)
					.addClass(data.stateClass)
					.text(data.stateText);
			};

			return this.each(function (index, el) {

				var $el = jquery(el);

				if ($el.data("state-switcher-params")) {

					$el.unbind("mouseenter", onEnter);
					$el.unbind("mouseleave", onLeave);
				}

				var data =
				{
					stateText: $el.text(),
					stateClass: $el.data("state-class"),
					actionText: $el.data("action-text"),
					actionClass: $el.data("action-class")
				};

				$el.data("state-switcher-params", data).addClass(data.stateClass);
				$el.hover(onEnter, onLeave);
			});
		};


		//#region handlebars helpers
		handlebars.registerHelper('range', function (from, to, incr, options) {

			var out = '', i;

			for (i = from; i <= to; i += incr) {

				out += options.fn(i);
			}

			return out;
		});

		handlebars.registerHelper('pad', function (value, length) {

			value = value + '';
			length = length || 0;

			return (Array(length + 1).join('0') + value).slice(-length);
		});

		//#endregion

		return {
			marionette: marionette,
			backbone: backbone,
			_: underscore,
			$: jquery,
			json2: json2,
			Chart: chartjs,
			syphon: syphon,
			handlebars: handlebars,
			moment: moment,
			utils: utils
		};
	});