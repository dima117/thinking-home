using System;
using System.ComponentModel.Composition;

namespace ThinkingHome.Plugins.NooLite
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class OnRX1164CommandReceivedAttribute : ExportAttribute
	{
		public OnRX1164CommandReceivedAttribute()
			: base("EB6000DD-79F1-408A-9325-4DCFFB1AD391", typeof(Action<int, int, byte[]>))
		{  
		}
	}
}
