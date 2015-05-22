define(['app',
		'webapp/mqtt/received-data-model',
		'webapp/mqtt/received-data-view'],
	function (application, models, views) {

		var api = {
			loadMessageList: function () {

				models.loadMessages()
					.done(function (model) {

						var view = new views.MessageList({
							model: model.info,
							collection: model.messages
						});

						application.setContentView(view);
					});
			}
		};

		return {
			start: api.loadMessageList
		};
	});