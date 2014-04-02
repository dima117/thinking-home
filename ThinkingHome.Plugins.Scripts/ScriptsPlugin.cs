using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.ClearScript.Windows;
using NHibernate.Linq;
using NHibernate.Mapping.ByCode;
using NLog;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Core.Plugins.Commands;
using ThinkingHome.Core.Plugins.Utils;
using ThinkingHome.Plugins.Listener;
using ThinkingHome.Plugins.Scripts.Data;
using ThinkingHome.Plugins.Scripts.Internal;

namespace ThinkingHome.Plugins.Scripts
{
	[Plugin]
	public class ScriptsPlugin : Plugin
	{
		private ScriptHost scriptHost;
		private readonly Dictionary<string, HashSet<string>> scriptEvents =
			new Dictionary<string, HashSet<string>>(StringComparer.CurrentCultureIgnoreCase);

		public override void Init()
		{
			var actions = new PluginMethodCollection();

			foreach (var action in ScriptExecuted)
			{
				actions.RegisterMethod(action.Metadata, action.Value);
			}

			scriptHost = new ScriptHost(actions, Logger, RunScript);

			foreach (var plugin in Context.GetAllPlugins())
			{
				GetScriptEvents(plugin);
			}
		}

		private void GetScriptEvents(Plugin plugin)
		{
			if (plugin == null)
			{
				return;
			}

			var properties = plugin.GetType()
				.GetProperties()
				.Where(m => m.PropertyType == typeof(ScriptEventHandlerDelegate[]))
				.ToList();

			foreach (var member in properties)
			{
				var eventInfo = member.GetCustomAttributes<ScriptEventAttribute>().SingleOrDefault();

				if (eventInfo != null)
				{
					if (!scriptEvents.ContainsKey(eventInfo.PluginAlias))
					{
						scriptEvents.Add(eventInfo.PluginAlias, new HashSet<string>(StringComparer.CurrentCultureIgnoreCase));
					}

					scriptEvents[eventInfo.PluginAlias].Add(eventInfo.EventAlias);
				}
			}
		}

		public override void InitDbModel(ModelMapper mapper)
		{
			mapper.Class<UserScript>(cfg => cfg.Table("Scripts_UserScript"));
			mapper.Class<ScriptEventHandler>(cfg => cfg.Table("Scripts_EventHandler"));
		}

		#region edit scripts

		[ExtCommand("Scripts", "GetEventList")]
		public object GetEvents(dynamic args)
		{
			var list = scriptEvents
				.SelectMany(x => x.Value, (x, y) => new { pluginAlias = x.Key, eventAlias = y })
				.ToList();

			return list;
		}

		[ExtCommand("Scripts", "GetSubscriptionList")]
		public object GetSubscriptions(dynamic args)
		{
			using (var session = Context.OpenSession())
			{
				var list = session.Query<ScriptEventHandler>()
					.Select(x => new
					{
						id = x.Id,
						scriptId = x.UserScript.Id,
						scriptName = x.UserScript.Name,
						pluginAlias = x.PluginAlias,
						eventAlias = x.EventAlias
					})
					.ToList();

				return list;
			}
		}

		[ExtCommand("Scripts", "GetScriptList")]
		public object GetScriptList(dynamic args)
		{
			using (var session = Context.OpenSession())
			{
				var list = session.Query<UserScript>()
					.Select(x => new { id = x.Id, name = x.Name })
					.ToArray();

				return list;
			}
		}

		[ExtCommand("Scripts", "AddScript")]
		public object AddScript(dynamic args)
		{
			var name = args.name;
			var body = args.body;

			using (var session = Context.OpenSession())
			{
				var guid = Guid.NewGuid();
				var script = new UserScript { Id = guid, Name = name, Body = body };
				session.Save(script);
				session.Flush();

				return guid;
			}
		}

		[ExtCommand("Scripts", "DeleteScript")]
		public object DeleteScript(dynamic args)
		{
			Guid scriptId = args.scriptId;

			using (var session = Context.OpenSession())
			{
				var subscription = session.Load<UserScript>(scriptId);
				session.Delete(subscription);
				session.Flush();
			}

			return null;
		}

		[ExtCommand("Scripts", "AddSubscription")]
		public object AddSubscription(dynamic args)
		{
			string pluginAlias = args.pluginAlias;
			string eventAlias = args.eventAlias;
			Guid scriptId = args.scriptId;

			using (var session = Context.OpenSession())
			{
				var guid = Guid.NewGuid();

				var script = session.Load<UserScript>(scriptId);

				var subscription = new ScriptEventHandler
				{
					Id = guid,
					PluginAlias = pluginAlias,
					EventAlias = eventAlias,
					UserScript = script
				};

				session.Save(subscription);
				session.Flush();

				return guid;
			}
		}

		[ExtCommand("Scripts", "DeleteSubscription")]
		public object DeleteSubscription(dynamic args)
		{
			Guid subscriptionId = args.subscriptionId;

			using (var session = Context.OpenSession())
			{
				var subscription = session.Load<ScriptEventHandler>(subscriptionId);
				session.Delete(subscription);
				session.Flush();
			}

			return null;
		}

		[ExtCommand("Scripts", "LoadScript")]
		public object LoadScript(dynamic args)
		{
			Guid id = args.id;

			using (var session = Context.OpenSession())
			{
				var script = session.Query<UserScript>()
					.Select(x => new { id = x.Id, name = x.Name, body = x.Body })
					.FirstOrDefault(x => x.id == id);

				return script;
			}
		}

		[ExtCommand("Scripts", "SaveScript")]
		public object SaveScript(dynamic args)
		{
			Guid id = args.id;
			string body = args.body;

			using (var session = Context.OpenSession())
			{
				var script = session.Get<UserScript>(id);
				script.Body = body;
				session.Flush();
			}

			return null;
		}

		#endregion

		#region run scripts

		[ImportMany("Scripts.ScriptExecuted")]
		public Lazy<Delegate, IExportCommandAttribute>[] ScriptExecuted { get; set; }

		[ExtCommand("Scripts", "Run")]
		public object RunScript(dynamic args)
		{
			string name = args.name;

			RunScript(name, null);

			return null;
		}

		[ScriptCommand("scripts", "run")]
		public void RunScript(string scriptName, object[] args)
		{
			using (var session = Context.OpenSession())
			{
				var script = session.Query<UserScript>().First(s => s.Name == scriptName);

				RunScript(script, args);
			}
		}

		public void RunScript(UserScript script, params object[] args)
		{
			ExecuteScript(script, scriptHost, Logger, args);
		}

		[Export("BE10460E-0E9E-4169-99BB-B1DE43B150FC", typeof(ScriptEventHandlerDelegate))]
		public void OnScriptEvent(string pluginAlias, string eventAlias, object[] args)
		{
			using (var session = Context.OpenSession())
			{
				var scripts = session.Query<ScriptEventHandler>()
					.Where(s => s.PluginAlias == pluginAlias && s.EventAlias == eventAlias)
					.Select(x => x.UserScript)
					.ToList();

				foreach (var script in scripts)
				{
					ExecuteScript(script, scriptHost, Logger, args);
				}
			}
		}

		private static void ExecuteScript(UserScript script, ScriptHost scriptHost, Logger logger, object[] args)
		{
			//Debugger.Launch();
			try
			{
				//var engine = new JScriptEngine(WindowsScriptEngineFlags.EnableDebugging);
				var engine = new JScriptEngine();
				engine.AddHostObject("host", scriptHost);

				string initArgsScript = string.Format("var arguments = {0};", args.ToJson("[]"));
				engine.Execute(initArgsScript);
				engine.Execute(script.Body);
			}
			catch (Exception ex)
			{
				var messge = string.Format("error in user script {0}", script.Name);
				logger.ErrorException(messge, ex);
			}
		}

		#endregion
	}
}
