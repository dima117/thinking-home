define(['lib'],
	function (lib) {

		var api = {
			loadMessages: function () {

				var defer = lib.$.Deferred();

				lib.$.getJSON('/api/mqtt/messages')
					.done(function (data) {

						var info = new lib.backbone.Model(data.info),
							messages = new lib.backbone.Collection(data.messages);

						defer.resolve({ info: info, messages: messages});
					})
					.fail(function () {

						defer.resolve(undefined);
					});

				return defer.promise();
			}
		};

		return api;
	});