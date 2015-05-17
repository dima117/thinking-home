using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace ThinkingHome.Plugins.Listener
{
	using AppFunc = Func<IDictionary<string, object>, Task>;

	public class Error404Module
	{
		private readonly AppFunc next;

		public Error404Module(AppFunc next)
		{
			if (next == null)
			{
				throw new ArgumentNullException("next");
			}

			this.next = next;
		}

		public Task Invoke(IDictionary<string, object> env)
		{
			var body = Encoding.UTF8.GetBytes("404");

			var response = new OwinResponse(env)
			{
				ReasonPhrase = "",
				StatusCode = 404,
				ContentType = "text/plain;charset=utf-8",
				ContentLength = body.Length
			};

			return response.WriteAsync(body);
		}
	}
}
