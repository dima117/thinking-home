using System;
using System.ComponentModel.Composition;
using ThinkingHome.Plugins.Listener.Api;

namespace ThinkingHome.Plugins.Listener
{
	public interface IHttpCommandAttribute
	{
		string Url { get; }
	}

	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class HttpCommandAttribute : ExportAttribute, IHttpCommandAttribute
	{
		public HttpCommandAttribute(string url)
			: base("Listener.RequestReceived", typeof(Func<HttpRequestParams, object>))
		{
			Url = url;
		}

		public string Url { get; private set; }
	}
}
