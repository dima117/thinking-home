define(
	['app', 'moment'],
	function(application) {

		application.module('Common', function (module, app, backbone, marionette, $, _) {
			
			module.utils = {
				formatString: function () {

					var s = arguments[0];
					for (var i = 0; i < arguments.length - 1; i++) {
						var reg = new RegExp("\\{" + i + "\\}", "gm");
						s = s.replace(reg, arguments[i + 1]);
					}

					return s;
				},
				alert: function () {
					var msg = module.utils.formatString.apply(null, arguments);
					window.alert(msg);
				},
				confirm: function () {
					var msg = module.utils.formatString.apply(null, arguments);
					return window.confirm(msg);
				},
				loadCss: function () {

					for (var i = 0; i < arguments.length; i++) {

						$('<link type="text/css" rel="stylesheet" />')
							.attr('href', arguments[i])
							.appendTo("head");
					}
				},
				displayCurrentTime: function(selector) {

					var $el = $(selector);
					
					if ($el.length) {

						return window.setInterval(function () {
							
							$el.text(moment().format('LT, ll'));
						}, 2000);
					}

					return undefined;
				}
			};
		});
		
		return application.Common;
	});

