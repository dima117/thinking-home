using System;
using System.ComponentModel.Composition;
using System.Web.Http;
using System.Web.Http.SelfHost;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Core.Plugins.Commands;
using ThinkingHome.Plugins.Listener.Api;

namespace ThinkingHome.Plugins.Listener
{
	/// <summary>
	/// http://localhost:8000/api/Scripts/Run?json={%22name%22%3A%22moo2%22}
	/// </summary>
	[Plugin]
	public class ListenerPlugin : Plugin
	{
		private const string BASE_URL_HTTP = "http://localhost:8000";

		private HttpSelfHostServer server;

		[ImportMany("Listener.RequestReceived")]
		public Lazy<Func<dynamic, object>, IExportCommandAttribute>[] RequestReceived { get; set; }

		public override void Init()
		{
			var actions = new HttpMethodCollection();
			foreach (var action in RequestReceived)
			{
				actions.RegisterMethod(action.Metadata, action.Value);
			}

			var config = new HttpSelfHostConfiguration(BASE_URL_HTTP)
			{
				DependencyResolver = new DependencyResolver(actions, Logger)
			};

			config.Routes.MapHttpRoute(
				"API Default", "api/{pluginName}/{methodName}/{callback}",
				new
				{
					controller = "Common", 
					action = "Get",
					callback = RouteParameter.Optional
				})
				.DataTokens["Namespaces"] = new[] {"ThinkingHome.Plugins.Listener.Api"};

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
	}
}
