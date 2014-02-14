using System;
using ThinkingHome.Core;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Core.Plugins.Commands;

namespace ThinkingHome.Plugins.Listener.Api
{
	public class HttpMethodCollection : MethodCollection<Func<dynamic, object>>
	{
	}
}