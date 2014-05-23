using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ThinkingHome.Core.Plugins;

namespace ThinkingHome.Plugins.Scripts
{
	public delegate void ScriptEventHandlerDelegate(string eventAlias, object[] parameters);

	public static class ScriptEventHelper
	{
		public static void RaiseScriptEvent<TPlugin>(this TPlugin plugin,
			Expression<Func<TPlugin, ScriptEventHandlerDelegate[]>> expression,
			params object[] parameters) where TPlugin : Plugin
		{
			var memberExpression = expression.Body as MemberExpression;
			if (memberExpression == null)
			{
				throw new InvalidOperationException("Expression must be a member expression");
			}

			var eventInfo = memberExpression.Member
				.GetCustomAttributes<ScriptEventAttribute>()
				.FirstOrDefault();

			if (eventInfo == null)
			{
				throw new InvalidOperationException("Event parameters not found");
			}

			var actions = expression.Compile()(plugin);

			plugin.Run(actions, action => action(eventInfo.EventAlias, parameters));
		}
	}
}
