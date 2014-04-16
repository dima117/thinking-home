using System;
using System.Collections.Generic;

namespace ThinkingHome.Plugins.Listener.Api
{
	public class HttpMethodCollection : Dictionary<string, Func<HttpRequestParams, object>>
	{
		private readonly object lockObject = new object();

		public HttpMethodCollection()
			: base(StringComparer.InvariantCultureIgnoreCase)
		{
		}

		public void RegisterMethod(IHttpCommandAttribute metadata, Func<HttpRequestParams, object> method)
		{
			if (metadata == null || method == null)
			{
				return;
			}

			lock (lockObject)
			{
				if (ContainsKey(metadata.Url))
				{
					var message = string.Format("duplicated handler for url: '{0}'", metadata.Url);
					throw new Exception(message);
				}

				Add(metadata.Url, method);
			}
		}
	}
}