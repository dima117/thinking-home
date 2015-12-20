using System.Reflection;

namespace ThinkingHome.Plugins.Listener.Attributes
{
	public interface IHttpResourceAttribute
	{
		byte[] GetContent(Assembly assembly);

		string ContentType { get; }
    }
}
