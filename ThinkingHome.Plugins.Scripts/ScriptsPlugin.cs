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
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Handlers;
using ThinkingHome.Plugins.Scripts.Data;
using ThinkingHome.Plugins.Scripts.Internal;
using ThinkingHome.Plugins.WebUI.Attributes;

namespace ThinkingHome.Plugins.Scripts
{
	// script list
	[AppSection("Scripts", SectionType.System, "/webapp/scripts/script-list.js", "ThinkingHome.Plugins.Scripts.Resources.script-list.js")]
	[JavaScriptResource("/webapp/scripts/script-list-model.js", "ThinkingHome.Plugins.Scripts.Resources.script-list-model.js")]
	[JavaScriptResource("/webapp/scripts/script-list-view.js", "ThinkingHome.Plugins.Scripts.Resources.script-list-view.js")]
	[HttpResource("/webapp/scripts/script-list-item.tpl", "ThinkingHome.Plugins.Scripts.Resources.script-list-item.tpl")]
	[HttpResource("/webapp/scripts/script-list.tpl", "ThinkingHome.Plugins.Scripts.Resources.script-list.tpl")]

	// editor
	[JavaScriptResource("/webapp/scripts/script-editor.js", "ThinkingHome.Plugins.Scripts.Resources.script-editor.js")]
	[JavaScriptResource("/webapp/scripts/script-editor-model.js", "ThinkingHome.Plugins.Scripts.Resources.script-editor-model.js")]
	[JavaScriptResource("/webapp/scripts/script-editor-view.js", "ThinkingHome.Plugins.Scripts.Resources.script-editor-view.js")]
	[HttpResource("/webapp/scripts/script-editor.tpl", "ThinkingHome.Plugins.Scripts.Resources.script-editor.tpl")]

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

		#region http scripts

		[HttpCommand("/api/scripts/list")]
		public object GetScriptList(HttpRequestParams request)
		{
			using (var session = Context.OpenSession())
			{
				var list = session.Query<UserScript>()
					.Select(x => new { id = x.Id, name = x.Name })
					.ToArray();

				return list;
			}
		}

		[HttpCommand("/api/scripts/delete")]
		public object DeleteScript(HttpRequestParams request)
		{
			Guid scriptId = request.GetRequiredGuid("scriptId");

			using (var session = Context.OpenSession())
			{
				var subscription = session.Load<UserScript>(scriptId);
				session.Delete(subscription);
				session.Flush();
			}

			return null;
		}

		[HttpCommand("/api/scripts/get")]
		public object LoadScript(HttpRequestParams request)
		{
			Guid id = request.GetRequiredGuid("id");

			using (var session = Context.OpenSession())
			{
				var script = session.Query<UserScript>()
					.Select(x => new { id = x.Id, name = x.Name, body = x.Body })
					.FirstOrDefault(x => x.id == id);

				return script;
			}
		}

		[HttpCommand("/api/scripts/save")]
		public object SaveScript(HttpRequestParams request)
		{
			Guid? id = request.GetGuid("id");
			string name = request.GetRequiredString("name");
			string body = request.GetRequiredString("body");

			using (var session = Context.OpenSession())
			{
				var script = id.HasValue
					? session.Get<UserScript>(id.Value)
					: new UserScript { Id = Guid.NewGuid() };

				script.Name = name;
				script.Body = body;
				session.SaveOrUpdate(script);
				session.Flush();
			}

			return null;
		}

		[HttpCommand("/api/scripts/run")]
		public object RunScript(HttpRequestParams request)
		{
			string name = request.GetRequiredString("name");

			RunScript(name, null);

			return null;
		}

		#endregion

		#region http subscriptions

		[HttpCommand("/api/scripts/events")]
		public object GetEvents(HttpRequestParams request)
		{
			var list = scriptEvents
				.SelectMany(x => x.Value, (x, y) => new { pluginAlias = x.Key, eventAlias = y })
				.ToList();

			return list;
		}

		[HttpCommand("/api/scripts/subscription/list")]
		public object GetSubscriptions(HttpRequestParams request)
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

		[HttpCommand("/api/scripts/subscription/add")]
		public object AddSubscription(HttpRequestParams request)
		{
			string pluginAlias = request.GetRequiredString("pluginAlias");
			string eventAlias = request.GetRequiredString("eventAlias");
			Guid scriptId = request.GetRequiredGuid("scriptId");

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

		[HttpCommand("/api/scripts/subscription/delete")]
		public object DeleteSubscription(HttpRequestParams request)
		{
			Guid subscriptionId = request.GetRequiredGuid("subscriptionId");

			using (var session = Context.OpenSession())
			{
				var subscription = session.Load<ScriptEventHandler>(subscriptionId);
				session.Delete(subscription);
				session.Flush();
			}

			return null;
		}

		#endregion

		#region run scripts

		[ImportMany("Scripts.ScriptExecuted")]
		public Lazy<Delegate, IExportCommandAttribute>[] ScriptExecuted { get; set; }

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
