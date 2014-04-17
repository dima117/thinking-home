using System;
using System.Collections.Generic;
using ThinkingHome.Plugins.Listener.Handlers;

namespace ThinkingHome.Plugins.Listener.Api
{
	public class HttpHandlerCollection : Dictionary<string, IListenerHandler>
	{
		private readonly object lockObject = new object();

		public HttpHandlerCollection()
			: base(StringComparer.InvariantCultureIgnoreCase)
		{
		}

		public void RegisterHandler(string url, IListenerHandler handler)
		{
			if (string.IsNullOrWhiteSpace(url) || handler == null)
			{
				return;
			}
			
			lock (lockObject)
			{
				if (ContainsKey(url))
				{
					var message = string.Format("duplicated handler for url: '{0}'", url);
					throw new Exception(message);
				}

				Add(url, handler);
			}
		}
	}
}