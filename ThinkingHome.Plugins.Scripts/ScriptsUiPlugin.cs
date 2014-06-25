using System;
using System.Linq;
using NHibernate.Linq;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.Scripts.Data;
using ThinkingHome.Plugins.WebUI.Attributes;

namespace ThinkingHome.Plugins.Scripts
{
	// css
	[CssResource("/webapp/scripts/codemirror.css", "ThinkingHome.Plugins.Scripts.Resources.codemirror.css", AutoLoad = true)]


	// script list
	[AppSection("Event handlers", SectionType.System, "/webapp/scripts/subscriptions.js", "ThinkingHome.Plugins.Scripts.Resources.subscriptions.js")]
	[JavaScriptResource("/webapp/scripts/subscriptions-model.js", "ThinkingHome.Plugins.Scripts.Resources.subscriptions-model.js")]
	[JavaScriptResource("/webapp/scripts/subscriptions-view.js", "ThinkingHome.Plugins.Scripts.Resources.subscriptions-view.js")]
	[HttpResource("/webapp/scripts/subscriptions-layout.tpl", "ThinkingHome.Plugins.Scripts.Resources.subscriptions-layout.tpl")]
	[HttpResource("/webapp/scripts/subscriptions-form.tpl", "ThinkingHome.Plugins.Scripts.Resources.subscriptions-form.tpl")]
	[HttpResource("/webapp/scripts/subscriptions-list.tpl", "ThinkingHome.Plugins.Scripts.Resources.subscriptions-list.tpl")]
	[HttpResource("/webapp/scripts/subscriptions-list-item.tpl", "ThinkingHome.Plugins.Scripts.Resources.subscriptions-list-item.tpl")]

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
	public class ScriptsUiPlugin : Plugin
	{
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
			string body = request.GetString("body");

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

		[HttpCommand("/api/scripts/run")]
		public object RunScript(HttpRequestParams request)
		{
			Guid scriptId = request.GetRequiredGuid("scriptId");

			using (var session = Context.OpenSession())
			{
				var script = session.Get<UserScript>(scriptId);

				Context.GetPlugin<ScriptsPlugin>().ExecuteScript(script, new object[0]);
			}

			return null;
		}

		#endregion

		#region http subscriptions

		[HttpCommand("/api/scripts/subscription/form")]
		public object GetSubscriptionForm(HttpRequestParams request)
		{
			using (var session = Context.OpenSession())
			{
				var events = Context.GetPlugin<ScriptsPlugin>().ScriptEvents
					.Select(eventAlias => new { id = eventAlias, name = eventAlias })
					.ToList();

				var scripts = session.Query<UserScript>()
					.Select(x => new { id = x.Id, name = x.Name })
					.ToArray();

				var selectedEventAlias = events.Any() ? events.First().id : null;
				var selectedScriptId = scripts.Any() ? (Guid?)scripts.First().id : null;

				return new
				{
					eventList = events,
					scriptList = scripts,
					selectedEventAlias,
					selectedScriptId
				};
			}
		}

		[HttpCommand("/api/scripts/subscription/list")]
		public object GetSubscriptionList(HttpRequestParams request)
		{
			using (var session = Context.OpenSession())
			{
				var list = session.Query<ScriptEventHandler>()
					.Select(x => new
					{
						id = x.Id,
						scriptId = x.UserScript.Id,
						scriptName = x.UserScript.Name,
						eventAlias = x.EventAlias
					})
					.ToList();

				return list;
			}
		}

		[HttpCommand("/api/scripts/subscription/add")]
		public object AddSubscription(HttpRequestParams request)
		{
			string eventAlias = request.GetRequiredString("eventAlias");
			Guid scriptId = request.GetRequiredGuid("scriptId");

			using (var session = Context.OpenSession())
			{
				var guid = Guid.NewGuid();

				var script = session.Load<UserScript>(scriptId);

				var subscription = new ScriptEventHandler
				{
					Id = guid,
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
	}
}
