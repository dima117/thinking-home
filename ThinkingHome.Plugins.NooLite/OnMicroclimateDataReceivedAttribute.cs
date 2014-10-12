using System;
using System.ComponentModel.Composition;

namespace ThinkingHome.Plugins.NooLite
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class OnMicroclimateDataReceivedAttribute : ExportAttribute
	{
		public OnMicroclimateDataReceivedAttribute()
			: base("A40F6290-A9C5-439B-9CDF-32656F8D1C65", typeof(Action<int, decimal, int>))
		{  
		}
	}
}