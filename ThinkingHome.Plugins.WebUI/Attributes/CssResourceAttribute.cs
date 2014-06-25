using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkingHome.Plugins.Listener.Attributes;

namespace ThinkingHome.Plugins.WebUI.Attributes
{
	public class CssResourceAttribute : HttpResourceAttribute
	{
		public bool AutoLoad { get; set; }

		public CssResourceAttribute(string url, string resourcePath)
			: base(url, resourcePath, "text/css")
		{
		}
	}
}
