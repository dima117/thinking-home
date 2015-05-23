define([
	'lib',
	'text!webapp/mqtt/received-data-list.tpl',
	'text!webapp/mqtt/received-data-list-item.tpl'],
	function (lib, tmplList, tmplListItem) {

		var messageListItemView = lib.marionette.ItemView.extend({
			template: lib._.template(tmplListItem),
			tagName: 'tr',
			triggers: {
				'click .js-delete': 'delete:message'
			}
		});

		var messageListView = lib.marionette.CompositeView.extend({
			template: lib._.template(tmplList),
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