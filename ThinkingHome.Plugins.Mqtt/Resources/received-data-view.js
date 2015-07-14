define([
	'lib',
	'text!webapp/mqtt/received-data-list.tpl',
	'text!webapp/mqtt/received-data-list-item.tpl'],
	function (lib, tmplList, tmplListItem) {

		var messageListItemView = lib.marionette.ItemView.extend({
			template: lib.handlebars.compile(tmplListItem),
			tagName: 'tr',
			triggers: {
				'click .js-delete': 'delete:message'
			}
		});

		var messageListView = lib.marionette.CompositeView.extend({
			template: lib.handlebars.compile(tmplList),
			childView: messageListItemView,
			childViewContainer: 'tbody',
			triggers: {
				'click .js-reload': 'reload:messages'
			}
		});

		return {
			MessageList: messageListView
		};
	});