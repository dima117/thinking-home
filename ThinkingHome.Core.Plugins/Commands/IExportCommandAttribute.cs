namespace ThinkingHome.Core.Plugins.Commands
{
	public interface IExportCommandAttribute
	{
		string PluginAlias { get; }
		string MethodAlias { get; }
	}
}