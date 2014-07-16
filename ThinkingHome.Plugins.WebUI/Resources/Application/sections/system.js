define(
	['app', 'application/sections/list'],
	function (application, sections) {

		application.module('WebUI.SystemSections', function (module, app, backbone, marionette, $, _) {

			module.start = function () {
				sections.api.reload('load:sections:system', 'System pages');
			};
		});

		return application.WebUI.SystemSections;
	});