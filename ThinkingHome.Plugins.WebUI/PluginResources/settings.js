define(
	['app', 'navigation', 'tpl!webapp/webui/settings.tpl', 'tpl!webapp/webui/table-row.tpl'],
	function (application, nav, template, rowTemplate) {

		application.module('WebUI.Settings', function (module, app, backbone, marionette, $, _) {

			module.TableRowView = marionette.ItemView.extend({
				template: rowTemplate,
				tagName: 'tr'
			});

			module.createView = function () {

				var defer = $.Deferred();
				var rows = new nav.NavItemCollection();

				rows.fetch({
					success: function (collection) {
						
						var view = new marionette.CompositeView({
							template: template,
							itemView: module.TableRowView,
							itemViewContainer: 'tbody',
							collection: collection
						});

						defer.resolve(view);
					},
					error: function () {
						defer.resolve(undefined);
					}
				});

				return defer.promise();
			};
		});

		return application.WebUI.Settings;
	});