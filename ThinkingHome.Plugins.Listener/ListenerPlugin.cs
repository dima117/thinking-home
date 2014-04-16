using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Web.Http;
using System.Web.Http.SelfHost;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Listener.Api;

namespace ThinkingHome.Plugins.Listener
{
	[Plugin]
	public class ListenerPlugin : Plugin
	{
		private const string BASE_URL_HTTP = "http://localhost:8000";

		private HttpSelfHostServer server;

		[ImportMany("Listener.RequestReceived")]
		public Lazy<Func<HttpRequestParams, object>, IHttpCommandAttribute>[] RequestReceived { get; set; }

		public override void Init()
		{
			var handlers = RegisterHandlers();
			var dependencyResolver = new DependencyResolver(handlers, Logger);

			var config = BuildConfiguration(dependencyResolver);

			server = new HttpSelfHostServer(config);
		}

		public override void Start()
		{
			server.OpenAsync();
		}

		public override void Stop()
		{
			server.Dispose();
		}

		#region private

		private HttpMethodCollection RegisterHandlers()
		{
			var handlers = new HttpMethodCollection();
			foreach (var action in RequestReceived)
			{
				Logger.Info("Register HTTP handler for url: '{0}'", action.Metadata.Url);
				handlers.RegisterMethod(action.Metadata, action.Value);
			}
			return handlers;
		}

		private static HttpSelfHostConfiguration BuildConfiguration(DependencyResolver dependencyResolver)
		{
			var config = new HttpSelfHostConfiguration(BASE_URL_HTTP)
			{
				DependencyResolver = dependencyResolver
			};

			var defaults = new { controller = "Common", action = "Index" };
			//var dataToken = new[] { "ThinkingHome.Plugins.Listener.Api" };

			Debugger.Launch();
			config.Routes.MapHttpRoute("Global", "{*url}", defaults);
				//.DataTokens["Namespaces"] = dataToken;
			return config;
		}

		#endregion
	}
}
