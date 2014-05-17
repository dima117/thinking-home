define(
	['app', 'webapp/webui/section-model', 'webapp/webui/section-view'],
	function (application) {

		application.module('WebUI.CommonSections', function (module, app, backbone, marionette, $, _) {

			var api = {

				reload: function () {

					app.request('load:sections:common').done(function (items) {
						var view = new app.WebUI.Sections.SectionListView({ collection: items, title: 'Common pages' });
						app.setContentView(view);
					});
				}
			};

			module.start = function () {
				api.reload();
			};

		});

		return application.WebUI.CommonSections;
	});