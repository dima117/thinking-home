using System;
using NLog;

namespace ThinkingHome.Plugins.Scripts.Internal
{
	public class ScriptHost
	{
		public readonly PluginMethodCollection methods;
		public readonly Logger logger;
		public readonly Action<string, object[]> scriptRunner;

		public ScriptHost(PluginMethodCollection methods, Logger logger, Action<string, object[]> scriptRunner)
		{
			this.methods = methods;
			this.logger = logger;
			this.scriptRunner = scriptRunner;
		}

		// ReSharper disable once InconsistentNaming
		public ScriptPluginWrapper getPlugin(string pluginName)
		{
			var actions = methods.GetMethods(pluginName);

			return actions != null ? new ScriptPluginWrapper(actions) : null;
		}

		// ReSharper disable once InconsistentNaming
		public void logInfo(object message, params object[] args)
		{
			logger.Log(LogLevel.Info, message.ToString(), args);
		}

		// ReSharper disable once InconsistentNaming
		public void logError(object message, params object[] args)
		{
			logger.Log(LogLevel.Error, message.ToString(), args);
		}

		// ReSharper disable once InconsistentNaming
		public void runScript(string name, params object[] args)
		{
			scriptRunner(name, args);
		}
	}
}