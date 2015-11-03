define(['lib'], function (lib) {
	var radio = lib.marionette.Object.extend({
		msgHubName: 'messageQueueHub',
		msgEventName: 'serverMessage',
		reconnectionTimeout: 7000,

		initialize: function () {
			this.connection = lib.$.hubConnection();
			this.connection.disconnected(lib._.bind(this.onDisconnect, this));

			this.hub = this.connection.createHubProxy(this.msgHubName);
			this.hub.on(this.msgEventName, lib._.bind(this.onMessage, this));
		},

		start: function () {
			this.connection.start();
		},
		onBeforeDestroy: function () {
			var connection = this.connection;
			delete this.connection;

			connection && connection.stop();
		},
		onDisconnect: function () {
			setTimeout(lib._.bind(function() {
				var connection = this.connection;
				connection && connection.start();
			}, this), this.reconnectionTimeout);
		},
		onMessage: function (message) {
			this.trigger(message.channel, message);
		},
		sendMessage: function (channel, data) {
			this.hub.invoke("Send", channel, data);
		}
	});

	return radio;
});