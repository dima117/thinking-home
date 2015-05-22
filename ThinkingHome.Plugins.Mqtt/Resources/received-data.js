define(['app',
		'webapp/mqtt/received-data-model',
		'webapp/mqtt/received-data-view'],
	function (application, models, views) {

		var api = {
			reloadMessages: function () {

				var collection = this.collection;

				models.loadMessages()
					.done(function (model) {

						var messages = model.messages.toJSON();
						collection.reset(messages);
					});
			},
			loadMessageList: function () {

				models.loadMessages()
					.done(function (model) {

						var view = new views.MessageList({
							model: model.info,
							collection: model.messages
						});

						view.on('reload:messages', api.reloadMessages);

						application.setContentView(view);
					});
			}
		};

		return {
			start: api.loadMessageList
		};
	});