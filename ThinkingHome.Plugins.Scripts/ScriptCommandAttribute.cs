using System;
using System.ComponentModel.Composition;

namespace ThinkingHome.Plugins.Scripts
{
	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class ScriptCommandAttribute : ExportAttribute, IScriptCommandAttribute
	{
		public string Alias { get; private set; }

		public ScriptCommandAttribute(string alias)
			: base("Scripts.ScriptExecuted", typeof(Delegate))
		{
			Alias = alias;
		}
	}
}
