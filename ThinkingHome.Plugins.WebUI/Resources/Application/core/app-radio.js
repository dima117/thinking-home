define(['lib'], function (lib) {
	var radio = lib.common.ApplicationBlock.extend({
		msgHubName: 'messageQueueHub',
		msgEventName: 'serverMessage',
		reconnectionTimeout: 7000,

		initialize: function () {
			this.connection = lib.$.hubConnection();
			this.connection.disconnected(this.bind('onDisconnect'));

			this.hub = this.connection.createHubProxy(this.msgHubName);
			this.hub.on(this.msgEventName, this.bind('onMessage'));
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
			this.connection && setTimeout(this.bind(function () {
				var connection = this.connection;
				connection && connection.start();
			}), this.reconnectionTimeout);
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