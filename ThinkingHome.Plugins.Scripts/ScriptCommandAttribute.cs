using System;
using System.ComponentModel.Composition;
using ThinkingHome.Core.Plugins.Commands;

namespace ThinkingHome.Plugins.Scripts
{
	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class ScriptCommandAttribute : ExportAttribute, IExportCommandAttribute
	{
		public ScriptCommandAttribute(string pluginAlias, string methodAlias)
			: base("Scripts.ScriptExecuted", typeof(Delegate))
		{
			PluginAlias = pluginAlias;
			MethodAlias = methodAlias;
		}

		public string PluginAlias { get; private set; }

		public string MethodAlias { get; private set; }
	}
}
