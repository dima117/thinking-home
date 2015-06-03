namespace ThinkingHome.Plugins.UniUI.Widgets
{
	public class WidgetSelectItem
	{
		public WidgetSelectItem(string id, string displayName)
		{
			Id = id;
			DisplayName = displayName;
		}

		public string Id { get; set; }

		public string DisplayName { get; set; }
	}
}
