using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkingHome.Plugins.Listener.Hubs
{
	public class MessageQueueHub : Hub
	{
		public void Send(string channel, object data)
		{
			var guid = Guid.NewGuid();
			var timestamp = DateTime.Now;

			Clients.All.serverMessage(new { guid, timestamp, channel, data });
		}
	}
}
