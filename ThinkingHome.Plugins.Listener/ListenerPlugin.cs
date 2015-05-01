using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Reflection;
using Owin;
using Microsoft.Owin.Hosting;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Core.Plugins.Utils;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.Listener.Handlers;

namespace ThinkingHome.Plugins.Listener
{
	[Plugin]
	public class ListenerPlugin : PluginBase
	{
		private const string BASE_URL_HTTP = "http://localhost:41831";

		private IDisposable server;
		private InternalDictionary<IListenerHandler> registeredHandlers;

		[ImportMany("5D358D8E-2310-49FE-A660-FB3ED7003B4C")]
		public Lazy<Func<HttpRequestParams, object>, IHttpCommandAttribute>[] RequestReceived { get; set; }

		public override void InitPlugin()
		{
			Debugger.Launch();
			registeredHandlers = RegisterHandlers();
		}

		public override void StartPlugin()
		{
			server = WebApp.Start(BASE_URL_HTTP, ConfigureModules);
		}

		private void ConfigureModules(IAppBuilder appBuilder)
		{
			appBuilder
				.Use<ListenerModule>(registeredHandlers, Logger)
				.UseErrorPage();
		}

		public override void StopPlugin()
		{
			server.Dispose();
			server = null;
		}

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
				
					var resHandler = new ResourceListenerHandler(
						type.Assembly, attribute.ResourcePath, attribute.ContentType);

					handlers.Register(attribute.Url, resHandler);
				}
			}

			return handlers;
		}

		#endregion
	}
}
