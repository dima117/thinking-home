define(
	['lib', 'moment'],
	function (lib, moment) {

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

					lib.$('<link type="text/css" rel="stylesheet" />')
						.attr('href', arguments[i])
						.appendTo("head");
				}
			},

			displayCurrentTime: function (selector) {

				var $el = lib.$(selector);
				var fn = function () {

					$el.text(moment().format('LT, ll'));
				};

				if ($el.length) {

					fn();
					return window.setInterval(fn, 2000);
				}

				return undefined;
			},

			addListItems: function(select, items, options) {

				var opt = lib._.extend({
					text: 'name',
					value: 'id'
				}, options);

				lib._.each(items, function (item) {

					var val = item[opt.value],
						text = item[opt.text];

					lib.$('<option />').val(val).text(text).appendTo(select);
				});
			}
		};

		// switchers
		lib.$.fn.stateSwitcher = function () {

			var onEnter = function () {

				var $el = lib.$(this),
					data = $el.data("state-switcher-params");

				$el.removeClass(data.stateClass)
				.addClass(data.actionClass)
				.text(data.actionText);
			},
			onLeave = function () {

				var $el = lib.$(this),
					data = $el.data("state-switcher-params");

				$el.removeClass(data.actionClass)
					.addClass(data.stateClass)
					.text(data.stateText);
			};

			return this.each(function (index, el) {

				var $el = lib.$(el);

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

		return utils;
	});

