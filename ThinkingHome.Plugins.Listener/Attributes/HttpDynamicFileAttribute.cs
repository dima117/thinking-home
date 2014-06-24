using System;
using System.ComponentModel.Composition;
using ThinkingHome.Plugins.Listener.Api;

namespace ThinkingHome.Plugins.Listener.Attributes
{
	public interface IHttpDynamicFileAttribute
	{
		string Url { get; }
		string ContentType { get; }
	}

	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class HttpDynamicFileAttribute : ExportAttribute, IHttpDynamicFileAttribute
	{
		public HttpDynamicFileAttribute(string url, string contentType)
			: base("B87D366E-4CE7-4442-A8FE-6A08F14C9736", typeof(Func<HttpRequestParams, byte[]>))
		{
			Url = url;
			ContentType = contentType;
		}

		public string Url { get; private set; }
		public string ContentType { get; private set; }
	}
}