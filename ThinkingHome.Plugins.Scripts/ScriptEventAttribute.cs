using System;
using System.ComponentModel.Composition;

namespace ThinkingHome.Plugins.Scripts
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public class ScriptEventAttribute : ImportManyAttribute
	{
		public string EventAlias { get; protected set; }

		public ScriptEventAttribute(string eventAlias)
			: base("BE10460E-0E9E-4169-99BB-B1DE43B150FC", typeof(ScriptEventHandlerDelegate))
		{
			EventAlias = eventAlias;
		}
	}
}
