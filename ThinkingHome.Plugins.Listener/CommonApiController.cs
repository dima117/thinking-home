using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using NLog;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Handlers;

namespace ThinkingHome.Plugins.Listener
{
	public class CommonController : ApiController
	{
		private readonly HttpHandlerCollection handlers;
		private readonly Logger logger;

		public CommonController(HttpHandlerCollection handlers, Logger logger)
		{
			this.handlers = handlers;
			this.logger = logger;
		}

		[HttpGet, HttpPost, HttpPut, HttpDelete]
		public HttpResponseMessage Index()
		{
			try
			{
				//Debugger.Launch();
				string localPath = Request.RequestUri.LocalPath;

				logger.Info("execute action: {0};", localPath);

				IListenerHandler handler;

				if (!handlers.TryGetValue(localPath, out handler))
				{
					var message = string.Format("handler for url '{0}' is not found", localPath);
					throw new Exception(message);
				}

				HttpRequestParams parameters = GetRequestParams(Request);
				HttpContent content = handler.ProcessRequest(parameters);

				return new HttpResponseMessage { Content = content };
			}
			catch (Exception ex)
			{
				logger.ErrorException(ex.Message, ex);
				throw;
			}
		}

		private static HttpRequestParams GetRequestParams(HttpRequestMessage request)
		{
			var urlData = HttpUtility.ParseQueryString(request.RequestUri.Query);

			var formData = new NameValueCollection();

			if (request.Content.IsFormData())
			{
				var task = request.Content.ReadAsFormDataAsync();
				task.Wait();
				formData = task.Result;
			}

			return new HttpRequestParams(urlData, formData);
		}
	}
}
