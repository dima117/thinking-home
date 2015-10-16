using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;

namespace ThinkingHome.Plugins.Listener.Hubs
{
	public class MessageQueueHub : Hub
	{
		public void Send(string channel, object data)
		{
			InternalSend(Clients, channel, data);
		}

		internal static void SendStatic(string channel, object data)
		{
			IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MessageQueueHub>();
			InternalSend(context.Clients, channel, data);
		}

		private static void InternalSend(IHubConnectionContext<dynamic> clients, string channel, object data)
		{
			var guid = Guid.NewGuid();
			var timestamp = DateTime.Now;

			clients.All.serverMessage(new { guid, timestamp, channel, data });
		}
	}
}
