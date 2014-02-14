using System;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;
using NLog;

namespace ThinkingHome.Plugins.Listener.Api
{
	public class CommonController : ApiController
	{
		private readonly HttpMethodCollection actions;
		private readonly Logger logger;

		public CommonController(HttpMethodCollection actions, Logger logger)
		{
			this.actions = actions;
			this.logger = logger;
		}

		[HttpGet, HttpPost]
		public HttpResponseMessage Get(string pluginName, string methodName, string callback = null, string json = "")
		{
			try
			{
				logger.Info("execute action: plugin name={0}; method name={1}; json={2}", pluginName, methodName, json);
				
				dynamic data = JsonConvert.DeserializeObject(json);

				var action = actions.GetMethod(pluginName, methodName);

				if (action == null)
				{
					var message = string.Format("invalid method {0}.{1}", pluginName, methodName);
					throw new Exception(message);
				}

				var obj = action(data);
				var jsonResult = JsonConvert.SerializeObject(obj);

				if (!string.IsNullOrWhiteSpace(callback))
				{
					jsonResult = string.Format("{0}({1})", callback, jsonResult);
				}

				var bytes = Encoding.UTF8.GetBytes(jsonResult);

				return new HttpResponseMessage { Content = new ByteArrayContent(bytes) };
			}
			catch (Exception ex)
			{
				logger.ErrorException(ex.Message, ex);
				throw;
			}
		}
	}
}
