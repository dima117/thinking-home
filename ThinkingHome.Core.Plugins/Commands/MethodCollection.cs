using System;
using System.Collections.Generic;

namespace ThinkingHome.Core.Plugins.Commands
{
	public class MethodCollection<T> : Dictionary<string, Dictionary<string, T>> where T : class
	{
		private readonly object lockObject = new object();

		public MethodCollection()
			: base(StringComparer.CurrentCultureIgnoreCase)
		{
		}

		public void RegisterMethod(IExportCommandAttribute metadata, T method)
		{
			if (metadata == null || method == null)
			{
				return;
			}

			lock (lockObject)
			{
				if (!ContainsKey(metadata.PluginAlias))
				{
					Add(metadata.PluginAlias, new Dictionary<string, T>(StringComparer.CurrentCultureIgnoreCase));
				}

				if (this[metadata.PluginAlias].ContainsKey(metadata.MethodAlias))
				{
					throw new Exception("duplicated method name");
				}

				this[metadata.PluginAlias].Add(metadata.MethodAlias, method);
			}
		}

		public T GetMethod(string pluginName, string methodName)
		{
			var methods = GetMethods(pluginName);

			if (methods != null && methods.ContainsKey(methodName))
			{
				return methods[methodName];
			}

			return null;
		}

		public Dictionary<string, T> GetMethods(string pluginName)
		{
			return ContainsKey(pluginName) ? this[pluginName] : null;
		}
	}
}
