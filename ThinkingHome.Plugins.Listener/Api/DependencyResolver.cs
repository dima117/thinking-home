using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using NLog;

namespace ThinkingHome.Plugins.Listener.Api
{
	public class DependencyResolver : IDependencyResolver
	{
		private readonly HttpMethodCollection actions;
		private readonly Logger logger;

		public DependencyResolver(HttpMethodCollection actions, Logger logger)
		{
			this.actions = actions;
			this.logger = logger;
		}
		
		public IDependencyScope BeginScope()
		{
			return this;
		}

		public object GetService(Type serviceType)
		{
			if (serviceType == typeof(CommonController))
			{
				return new CommonController(actions, logger);
			}
			
			return null;
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return new List<object>();
		}

		public void Dispose()
		{
		}
	}
}
