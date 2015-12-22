using System;
using System.ComponentModel.Composition;
using System.Reflection;
using Owin;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Core.Plugins.Utils;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.Listener.Handlers;
using System.Configuration;
using ThinkingHome.Plugins.Listener.Hubs;

namespace ThinkingHome.Plugins.Listener
{
	[Plugin]
	public class ListenerPlugin : PluginBase
	{
		private const string BASE_URL_HTTP_FORMAT = "http://+:{0}";
        
        private static string BaseUrl 
        {
            get 
            {
                var port = ConfigurationManager.AppSettings["Listner.Port"] ?? "41831";
                return string.Format(BASE_URL_HTTP_FORMAT, port);
            }
        }

		private IDisposable server;
		private InternalDictionary<IListenerHandler> registeredHandlers;

		[ImportMany("5D358D8E-2310-49FE-A660-FB3ED7003B4C")]
		public Lazy<Func<HttpRequestParams, object>, IHttpCommandAttribute>[] RequestReceived { get; set; }

		public override void InitPlugin()
		{
			registeredHandlers = RegisterHandlers();
		}

		public override void StartPlugin()
		{
			server = WebApp.Start(BaseUrl, ConfigureModules);
		}

		private void ConfigureModules(IAppBuilder appBuilder)
		{
			appBuilder
				.Use<ListenerModule>(registeredHandlers, Logger)
				.MapSignalR(new HubConfiguration { EnableJavaScriptProxies = false })
				.Use<Error404Module>();
		}

		public override void StopPlugin()
		{
			server.Dispose();
			server = null;
		}

		#region public api

		public void Send(string channel, object data)
		{
			MessageQueueHub.SendStatic(channel, data);
		}

		#endregion

		#region private

		private InternalDictionary<IListenerHandler> RegisterHandlers()
		{
			var handlers = new InternalDictionary<IListenerHandler>();

			// регистрируем обработчики для методов плагинов
			foreach (var action in RequestReceived)
			{
				Logger.Info("Register HTTP handler (API): '{0}'", action.Metadata.Url);

				var handler = new ApiListenerHandler(action.Value);
				handlers.Register(action.Metadata.Url, handler);
			}

			// регистрируем обработчики для ресурсов
			foreach (var plugin in Context.GetAllPlugins())
			{
				Type type = plugin.GetType();
				var attributes = type.GetCustomAttributes<HttpResourceAttribute>();

				foreach (var attribute in attributes)
				{
					Logger.Info("Register HTTP handler (resource): '{0}'", attribute.Url);
				
					var resHandler = new ResourceListenerHandler(type.Assembly, attribute);

					handlers.Register(attribute.Url, resHandler);
				}
			}

			return handlers;
		}

		#endregion
	}
}
