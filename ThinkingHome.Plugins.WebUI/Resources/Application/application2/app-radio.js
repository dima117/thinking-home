define(['lib'], function (lib) {
	var radio = lib.marionette.Object.extend({
		
		initialize: function () {

			this.hub = lib.$.connection.messageQueueHub;
			this.hub.client.serverMessage = lib._.bind(this.onMessage, this);
		},

		start: function () {
			$.connection.hub.start()
				.done(function () { console.log('done'); })
				.fail(function () { console.log('fail', arguments); });
		},
		onBeforeDestroy: function () {

		},
		onMessage: function (message) {
			this.trigger(message.channel, message);
		},
		sendMessage: function (channel, data) {
			this.hub.invoke("Send", channel, data);
		}
	});

	return {
		Radio: radio
	};
});