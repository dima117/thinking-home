define(
	['app', 'navigation', 'tpl!webapp/webui/settings.tpl', 'tpl!webapp/webui/table-row.tpl'],
	function (application, nav, template, rowTemplate) {

		application.module('WebUI.Settings', function (module, app, backbone, marionette, $, _) {

			module.TableRowView = marionette.ItemView.extend({
				template: rowTemplate,
				tagName: 'tr'
			});

			module.createView = function () {

				var rows = new nav.NavItemCollection();

				var view = new marionette.CompositeView({
					template: template,
					itemView: module.TableRowView,
					itemViewContainer: 'tbody',
					collection: rows
				});

				rows.fetch();

				return view;
			};
		});

		return application.WebUI.Settings;
	});