using System;
using System.ComponentModel.Composition;

namespace ThinkingHome.Plugins.Timer
{

	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class OnTimerElapsedAttribute : ExportAttribute
	{
		public OnTimerElapsedAttribute()
			: base("E62C804C-B96B-4CA8-822E-B1725B363534", typeof(Action<DateTime>))
		{  
		}
	}
}
