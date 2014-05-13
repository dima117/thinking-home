define(
	['app'],
	function (application) {

		application.module('Common', function (module, app, backbone, marionette, $, _) {

			module.SubApplication = function (moduleName, startPage, pages) {

				pages = pages || {};
				
				var cm = app.module(moduleName, function (m, a, bb, mjs, s, l) {

					var api = {
						load: function (name, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) {

							var path = pages[name] || name;

							require([path], function (obj) {

								obj.on('subapp:open', api.load);
								obj.start(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
							});
						}
					};

					m.start = function() {
						api.load(startPage);
					};
				});
				
				return cm;
			};
		});

		return application.Common;
	});