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
			: base("41AAE5E9-50CE-46E9-AE54-5A4DF4049846", typeof(Delegate))
		{
			Alias = alias;
		}
	}
}
