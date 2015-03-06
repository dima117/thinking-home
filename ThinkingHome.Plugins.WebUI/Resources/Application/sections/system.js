define(
	['app', 'application/sections/list'],
	function (application, sections) {

		application.module('WebUI.SystemSections', function (module, app, backbone, marionette, $, _) {

			module.start = function () {
				sections.api.reload('loadSystemSections', 'System pages');
			};
		});

		return application.WebUI.SystemSections;
	});