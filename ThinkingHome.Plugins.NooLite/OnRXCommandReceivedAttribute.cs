using System;
using System.ComponentModel.Composition;

namespace ThinkingHome.Plugins.NooLite
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class OnRXCommandReceivedAttribute : ExportAttribute
	{
		public OnRXCommandReceivedAttribute()
			: base("EB6000DD-79F1-408A-9325-4DCFFB1AD391", typeof(Action<int, int, byte[]>))
		{  
		}
	}
}
