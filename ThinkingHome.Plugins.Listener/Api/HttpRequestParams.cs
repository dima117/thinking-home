using System;
using System.Collections.Specialized;

namespace ThinkingHome.Plugins.Listener.Api
{
	public class HttpRequestParams
	{
		public readonly NameValueCollection UrlData;
		public readonly NameValueCollection FormData;

		public HttpRequestParams(NameValueCollection urlData, NameValueCollection formData)
		{
			UrlData = urlData;
			FormData = formData;
		}

		public string GetString(string name)
		{
			throw new NotImplementedException();
		}

		public int? GetInt32(string name)
		{
			throw new NotImplementedException();
		}

		public Guid? GetGuid(string name)
		{
			throw new NotImplementedException();
		}

		public string GetRequiredString(string name)
		{
			throw new NotImplementedException();
		}

		public int GetRequiredInt32(string name)
		{
			throw new NotImplementedException();
		}

		public Guid GetRequiredGuid(string name)
		{
			throw new NotImplementedException();
		}
	}
}
