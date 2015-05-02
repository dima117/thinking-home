using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Newtonsoft.Json;
using ThinkingHome.Plugins.Listener.Api;

namespace ThinkingHome.Plugins.Listener.Handlers
{
	public class ApiListenerHandler : IListenerHandler
	{
		private readonly Func<HttpRequestParams, object> action;

		public ApiListenerHandler(Func<HttpRequestParams, object> action)
		{
			if (action == null)
			{
				throw new NullReferenceException();
			}

			this.action = action;
		}

		public Task ProcessRequest(OwinRequest request)
		{
			var parameters = GetRequestParams(request);
			var result = action(parameters);
			var json = JsonConvert.SerializeObject(result);
			var jsonBytes = Encoding.UTF8.GetBytes(json);

			var response = new OwinResponse(request.Environment)
			{
				Headers =
				{
					{"Cache-Control", new []{"no-store", "no-cache"}},
					{"Pragma", new []{"no-cache"}}
				},
				ContentType = "application/json;charset=utf-8",
				ContentLength = jsonBytes.Length
			};

			return response.WriteAsync(jsonBytes);
		}

		private static HttpRequestParams GetRequestParams(OwinRequest request)
		{
			var task = request.ReadFormAsync();
			task.Wait();

			return new HttpRequestParams(request.Query, task.Result);
		}
	}
}
