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
			}
		};


		return utils;
	});

