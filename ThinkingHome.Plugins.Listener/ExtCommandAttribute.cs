using System;
using System.ComponentModel.Composition;
using ThinkingHome.Core.Plugins.Commands;

namespace ThinkingHome.Plugins.Listener
{
	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class ExtCommandAttribute : ExportAttribute, IExportCommandAttribute
	{
		public ExtCommandAttribute(string pluginAlias, string methodAlias)
			: base("Listener.RequestReceived", typeof(Func<dynamic, object>))
		{
			PluginAlias = pluginAlias;
			MethodAlias = methodAlias;
		}

		public string PluginAlias { get; private set; }

		public string MethodAlias { get; private set; }
	}
}
