define([
	'lib',
	'text!webapp/mqtt/received-data-list.tpl',
	'text!webapp/mqtt/received-data-list-item.tpl',
	'lang!webapp/mqtt/lang.json'],
	function (lib, tmplList, tmplListItem, lang) {

		var messageListItemView = lib.marionette.ItemView.extend({
			template: lib.handlebars.compile(tmplListItem),
			tagName: 'tr',
			triggers: { 'click .js-delete': 'delete:message' },
			templateHelpers: { lang: lang }
		});

		var messageListView = lib.marionette.CompositeView.extend({
			template: lib.handlebars.compile(tmplList),
			childView: messageListItemView,
			childViewContainer: 'tbody',
			triggers: { 'click .js-reload': 'reload:messages' },
			templateHelpers: { lang: lang }
		});

		return {
			MessageList: messageListView
		};
	});