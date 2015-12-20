using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Owin;
using ThinkingHome.Plugins.Listener.Attributes;

namespace ThinkingHome.Plugins.Listener.Handlers
{
	public class ResourceListenerHandler : IListenerHandler
	{
		private readonly object lockObject = new object();
		private WeakReference<byte[]> resourceReference;

		private readonly Assembly assembly;
		private readonly IHttpResourceAttribute data;

		public ResourceListenerHandler(Assembly assembly, IHttpResourceAttribute data)
		{
			this.assembly = assembly;
			this.data = data;
		}

		public Task ProcessRequest(OwinRequest request)
		{
			byte[] resource = GetResource();

			var response = new OwinResponse(request.Environment)
			{
				ContentType = data.ContentType,
				ContentLength = resource.Length
			};

			return response.WriteAsync(resource);
		}

		private byte[] GetResource()
		{
			byte[] result;

			if (resourceReference == null || !resourceReference.TryGetTarget(out result))
			{
				lock (lockObject)
				{
					if (resourceReference == null || !resourceReference.TryGetTarget(out result))
					{
						result = data.GetContent(assembly);
						resourceReference = new WeakReference<byte[]>(result);
					}
				}
			}

			return result;
		}
	}
}
