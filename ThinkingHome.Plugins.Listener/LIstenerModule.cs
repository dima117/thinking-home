using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;
using NLog;
using ThinkingHome.Core.Plugins.Utils;
using ThinkingHome.Plugins.Listener.Handlers;

namespace ThinkingHome.Plugins.Listener
{
	using AppFunc = Func<IDictionary<string, object>, Task>;

	public class ListenerModule
	{
		private readonly InternalDictionary<IListenerHandler> handlers;
		private readonly Logger logger;
		private readonly AppFunc next;

		public ListenerModule(AppFunc next, InternalDictionary<IListenerHandler> handlers, Logger logger)
        {
			if (next == null)
			{
				throw new ArgumentNullException("next");
			}
 
			this.handlers = handlers;
			this.logger = logger;
			this.next = next;
		}

		public Task Invoke(IDictionary<string, object> env)
		{
			try
			{
				var request = new OwinRequest(env);
				var path = request.Path.ToString();

				logger.Info("execute action: {0};", path);

				IListenerHandler handler;

				if (handlers.TryGetValue(path, out handler))
				{
					var response = handler.ProcessRequest(request);

					//var message = string.Format("handler for url '{0}' is not found", localPath);
					var tcs = new TaskCompletionSource<object>();
					tcs.SetResult(response);
					return tcs.Task;
				}
			}
			catch (Exception ex)
			{
				var tcs = new TaskCompletionSource<object>();
				tcs.SetException(ex);
				logger.ErrorException("", ex);
				return tcs.Task;
			}

			return next(env);
		}
	}
}
