define(
	['app', 'application/sections/list-model', 'application/sections/list-view'],
	function (application) {

		application.module('WebUI.Sections', function (module, app, backbone, marionette, $, _) {

			module.api = {

				reload: function (requestName, pageTitle) {

					app.request(requestName).done(function (items) {
						var view = new module.SectionListView({ collection: items, title: pageTitle });
						app.setContentView(view);
					});
				}
			};
		});

		return application.WebUI.Sections;
	});