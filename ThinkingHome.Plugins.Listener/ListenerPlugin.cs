using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.SelfHost;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Handlers;

namespace ThinkingHome.Plugins.Listener
{
	[Plugin]
	public class ListenerPlugin : Plugin
	{
		private const string BASE_URL_HTTP = "http://localhost:41831";

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

		private HttpHandlerCollection RegisterHandlers()
		{
			var handlers = new HttpHandlerCollection();

			// регистрируем обработчики для методов плагинов
			foreach (var action in RequestReceived)
			{
				Logger.Info("Register HTTP handler (API): '{0}'", action.Metadata.Url);

				var handler = new ApiListenerHandler(action.Value);
				handlers.RegisterHandler(action.Metadata.Url, handler);
			}

			// регистрируем обработчики для ресурсов
			foreach (Plugin plugin in Context.GetAllPlugins())
			{
				Type type = plugin.GetType();
				var attributes = type.GetCustomAttributes<HttpResourceAttribute>();

				foreach (var attribute in attributes)
				{
					Logger.Info("Register HTTP handler (resource): '{0}'", attribute.Url);
				
					var resHandler = new ResourceListenerHandler(
						type.Assembly, attribute.ResourcePath, attribute.ContentType);

					handlers.RegisterHandler(attribute.Url, resHandler);
				}
			}

			return handlers;
		}

		private HttpSelfHostConfiguration BuildConfiguration(DependencyResolver dependencyResolver)
		{
			var config = new HttpSelfHostConfiguration(BASE_URL_HTTP)
			{
				DependencyResolver = dependencyResolver
			};

			var defaults = new { controller = "Common", action = "Index" };

			config.Routes.MapHttpRoute("Global", "{*url}", defaults);

			return config;
		}

		#endregion
	}
}
