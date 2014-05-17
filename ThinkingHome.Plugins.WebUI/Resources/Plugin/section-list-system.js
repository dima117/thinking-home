define(
	['app', 'webapp/webui/section-model', 'webapp/webui/section-view'],
	function (application) {

		application.module('WebUI.SystemSections', function (module, app, backbone, marionette, $, _) {

			var api = {

				reload: function () {

					app.request('load:sections:system').done(function (items) {
						var view = new app.WebUI.Sections.SectionListView({ collection: items, title: 'System pages' });
						app.setContentView(view);
					});
				}
			};

			module.start = function () {
				api.reload();
			};

		});

		return application.WebUI.SystemSections;
	});