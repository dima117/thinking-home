define(
	['app'],
	function (application) {

		application.module('Common', function (module, app, backbone, marionette, $, _) {

			module.SubApplication = function (moduleName, startPage, pages) {

				pages = pages || {};
				
				var cm = app.module(moduleName, function (m, a, bb, mjs, s, l) {

					var api = {
						load: function (name) {

							var path = pages[name] || name;
							var args = Array.prototype.slice.call(arguments, 1);

							require([path], function (obj) {

								obj.on('subapp:open', api.load);
								obj.start.apply(obj, args);
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