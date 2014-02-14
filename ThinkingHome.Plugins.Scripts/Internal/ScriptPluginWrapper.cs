using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ThinkingHome.Plugins.Scripts.Internal
{
	public class ScriptPluginWrapper
	{
		private readonly Dictionary<string, Delegate> methods;

		public ScriptPluginWrapper(Dictionary<string, Delegate> methods)
		{
			this.methods = methods;
		}

		// ReSharper disable once InconsistentNaming
		public object executeMethod(string method, params object[] args)
		{
			return methods[method].DynamicInvoke(args);
		}
	}
}