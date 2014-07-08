using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using Microsoft.ClearScript.Windows;
using NHibernate.Linq;
using NHibernate.Mapping.ByCode;
using NLog;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Core.Plugins.Utils;
using ThinkingHome.Plugins.Scripts.Data;

namespace ThinkingHome.Plugins.Scripts
{
	[Plugin]
	public class ScriptsPlugin : Plugin
	{
		private ScriptHost scriptHost;
		private HashSet<string> scriptEvents;

		public override void InitPlugin()
		{
			var actions = RegisterScriptCommands();

			scriptHost = new ScriptHost(actions, Logger, ExecuteScriptByName);
			scriptEvents = RegisterScriptEvents(Context.GetAllPlugins(), Logger);
		}

		private InternalDictionary<Delegate> RegisterScriptCommands()
		{
			var actions = new InternalDictionary<Delegate>();

			foreach (var action in ScriptCommands)
			{
				actions.Register(action.Metadata.Alias, action.Value);
			}

			return actions;
		}

		private static HashSet<string> RegisterScriptEvents(IEnumerable<Plugin> plugins, Logger logger)
		{
			var scriptEvents = new HashSet<string>(StringComparer.CurrentCultureIgnoreCase);
			
			foreach (var plugin in plugins)
			{
				var properties = plugin.GetType()
					.GetProperties()
					.Where(m => m.PropertyType == typeof(ScriptEventHandlerDelegate[]))
					.ToList();

				foreach (var member in properties)
				{
					var eventInfo = member.GetCustomAttributes<ScriptEventAttribute>().SingleOrDefault();

					if (eventInfo != null)
					{
						logger.Info("register script event '{0}' ({1})", eventInfo.EventAlias, member);

						if (scriptEvents.Contains(eventInfo.EventAlias))
						{
							var message = string.Format("duplicate event alias: '{0}'", eventInfo.EventAlias);
							throw new Exception(message);
						}

						scriptEvents.Add(eventInfo.EventAlias);
					}
				}
			}

			return scriptEvents;
		}

		public override void InitDbModel(ModelMapper mapper)
		{
			mapper.Class<UserScript>(cfg => cfg.Table("Scripts_UserScript"));
			mapper.Class<ScriptEventHandler>(cfg => cfg.Table("Scripts_EventHandler"));
		}

		#region public

		public ReadOnlyCollection<string> ScriptEvents
		{
			get { return scriptEvents.ToList().AsReadOnly(); }
		}

		/// <summary>
		/// Запуск скриптов (для плагинов)
		/// </summary>
		/// <param name="script"></param>
		/// <param name="args"></param>
		public void ExecuteScript(UserScript script, params object[] args)
		{
			ExecuteScript(script, scriptHost, Logger, args);
		}

		#endregion

		#region private

		/// <summary>
		/// Методы плагинов, доступные для скриптов
		/// </summary>
		[ImportMany("41AAE5E9-50CE-46E9-AE54-5A4DF4049846")]
		public Lazy<Delegate, IScriptCommandAttribute>[] ScriptCommands { get; set; }


		/// <summary>
		/// Запуск скриптов по имени (из других скриптов)
		/// </summary>
		private void ExecuteScriptByName(string scriptName, object[] args)
		{
			using (var session = Context.OpenSession())
			{
				var script = session.Query<UserScript>().First(s => s.Name == scriptName);

				ExecuteScript(script, args);
			}
		}

		/// <summary>
		/// Запуск скриптов, подписанных на события
		/// </summary>
		[Export("BE10460E-0E9E-4169-99BB-B1DE43B150FC", typeof(ScriptEventHandlerDelegate))]
		public void OnScriptEvent(string eventAlias, object[] args)
		{
			using (var session = Context.OpenSession())
			{
				var scripts = session.Query<ScriptEventHandler>()
					.Where(s => s.EventAlias == eventAlias)
					.Select(x => x.UserScript)
					.ToList();

				foreach (var script in scripts)
				{
					ExecuteScript(script, scriptHost, Logger, args);
				}
			}
		}

		/// <summary>
		/// Запуск скрипта
		/// </summary>
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
