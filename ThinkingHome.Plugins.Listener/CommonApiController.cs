using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using NLog;
using ThinkingHome.Plugins.Listener.Api;

namespace ThinkingHome.Plugins.Listener
{
	public class CommonController : ApiController
	{
		private readonly HttpMethodCollection handlers;
		private readonly Logger logger;

		public CommonController(HttpMethodCollection handlers, Logger logger)
		{
			this.handlers = handlers;
			this.logger = logger;
		}

		[HttpGet, HttpPost, HttpPut, HttpDelete]
		public HttpResponseMessage Index()
		{
			try
			{
				Debugger.Launch();
				logger.Info("execute action: {0};", Request.RequestUri.LocalPath);

				var action = handlers[Request.RequestUri.LocalPath];

				if (action == null)
				{
					var message = string.Format("handler for url '{0}' is not found", Request.RequestUri.LocalPath);
					throw new Exception(message);
				}

				HttpRequestParams parameters = GetRequestParams(Request);
				object obj = action(parameters);

				var jsonResult = JsonConvert.SerializeObject(obj);

				//if (!string.IsNullOrWhiteSpace(callback))
				//{
				//	jsonResult = string.Format("{0}({1})", callback, jsonResult);
				//}

				return new HttpResponseMessage { Content = new StringContent(jsonResult, Encoding.UTF8, "application/json") };
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
