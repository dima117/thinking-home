define(['app', 'common',
		'webapp/mqtt/received-data-model',
		'webapp/mqtt/received-data-view'],
	function (application, common, models, views) {

		var api = {
			reloadMessages: function () {

				var collection = api.view.collection;

				models.loadMessages()
					.done(function (model) {

						var messages = model.messages.toJSON();
						collection.reset(messages);
					});
			},
			deleteMessage: function (childView) {

				var path = childView.model.get("path");

				if (common.utils.confirm('Do you want to delete saved message?\n"{0}"', path)) {

					var id = childView.model.get('id');

					models.deleteMessage(id).done(api.reloadMessages);
				}
			},
			loadMessageList: function () {

				models.loadMessages()
					.done(function (model) {

						var view = new views.MessageList({
							model: model.info,
							collection: model.messages
						});

						view.on('reload:messages', api.reloadMessages);
						view.on('childview:delete:message', api.deleteMessage);

						api.view = view;
						application.setContentView(view);
					});
			}
		};

		return {
			start: api.loadMessageList
		};
	});