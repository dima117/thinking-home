define(['lib',
		'webapp/mqtt/received-data-model',
		'webapp/mqtt/received-data-view',
		'lang!webapp/mqtt/lang.json'],
	function (lib, models, views, lang) {

		var messageList = lib.common.AppSection.extend({
			start: function () {
				this.loadMessageList();
			},

			reloadMessages: function (view) {
				models.loadMessages()
					.done(function (model) {
						var messages = model.messages.toJSON();
						view.collection.reset(messages);
					});
			},

			deleteMessage: function (view, childView) {
				var path = childView.model.get("path");

				if (lib.utils.confirm(lang.get('Do_you_want_to_delete_saved_message_0'), path)) {
					var id = childView.model.get('id');
					models.deleteMessage(id).done(this.bind('reloadMessages', view));
				}
			},

			displayMessageList: function (model) {
				var view = new views.MessageList({
					model: model.info,
					collection: model.messages
				});

				this.listenTo(view, 'reload:messages', this.bind('reloadMessages', view));
				this.listenTo(view, 'childview:delete:message', this.bind('deleteMessage', view));
				this.listenTo(this.application.radio, 'mqtt:message', this.bind('reloadMessages', view))

				this.application.setContentView(view);
			},

			loadMessageList: function () {
				models.loadMessages().done(this.bind('displayMessageList'));
			}
		});

		return messageList;
	});