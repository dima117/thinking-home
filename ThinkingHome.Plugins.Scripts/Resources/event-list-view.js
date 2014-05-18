define(
	['app',
		'tpl!webapp/scripts/event-list-layout.tpl',
		'tpl!webapp/scripts/event-handler-list.tpl',
		'tpl!webapp/scripts/event-handler-list-item.tpl'],
	function (application, layoutTemplate, listTemplate, itemTemplate) {

		application.module('Scripts.EventList', function (module, app, backbone, marionette, $, _) {

			module.EventHandlerView = marionette.ItemView.extend({
				template: itemTemplate,
				tagName: 'tr'
			});

			module.EventHandlerListView = marionette.CompositeView.extend({
				template: listTemplate,
				itemView: module.EventHandlerView,
				itemViewContainer: 'tbody'
			});
			
			module.EventsLayout = marionette.Layout.extend({
				template: layoutTemplate,
				regions: {
					regionList: '#event-handler-list'
				}
			});
		});

		return application.Scripts.EventList;
	});