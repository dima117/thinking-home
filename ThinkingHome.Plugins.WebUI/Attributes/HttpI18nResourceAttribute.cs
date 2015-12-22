using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using ThinkingHome.Core.Plugins.Utils;
using ThinkingHome.Plugins.Listener.Attributes;

namespace ThinkingHome.Plugins.WebUI.Attributes
{
	public class HttpI18NResourceAttribute : HttpResourceAttribute
	{
		private readonly string baseName;

		public HttpI18NResourceAttribute(string url, string baseName) 
			: base(url, "application/json;charset=utf-8")
		{
			this.baseName = baseName;
		}

		public override byte[] GetContent(Assembly assembly)
		{
			var result = new Dictionary<string, string>();

			var resourceManager = new ResourceManager(baseName, assembly);

			using (var resourceSet = resourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true))
			{
				foreach (DictionaryEntry entry in resourceSet)
				{
					var key = entry.Key.ToString();
					result.Add(key, resourceManager.GetString(key));
				}
			}

			var json = result.ToJson();
			return Encoding.UTF8.GetBytes(json);
		}
	}
}
