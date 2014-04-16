using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using NLog;

namespace ThinkingHome.Plugins.Listener.Api
{
	public class DependencyResolver : IDependencyResolver
	{
		private readonly HttpMethodCollection handlers;
		private readonly Logger logger;

		public DependencyResolver(HttpMethodCollection handlers, Logger logger)
		{
			this.handlers = handlers;
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
				return new CommonController(handlers, logger);
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
