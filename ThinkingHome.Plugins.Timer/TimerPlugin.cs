using System;
using System.ComponentModel.Composition;
using System.Timers;
using ThinkingHome.Core.Plugins;

namespace ThinkingHome.Plugins.Timer
{
	[Plugin]
	public class TimerPlugin : PluginBase
	{
		private const int TIMER_INTERVAL = 30000;

		private System.Timers.Timer timer;

		public override void InitPlugin()
		{
			timer = new System.Timers.Timer(TIMER_INTERVAL);
			timer.Elapsed += OnTimedEvent;
		}

		[ImportMany("E62C804C-B96B-4CA8-822E-B1725B363534")]
		public Action<DateTime>[] OnEvent { get; set; }

		public override void StartPlugin()
		{
			timer.Enabled = true;
		}

		public override void StopPlugin()
		{
			timer.Enabled = false;
		}

		private void OnTimedEvent(object source, ElapsedEventArgs e)
		{
			var now = DateTime.Now;
			Run(OnEvent, x => x(now));
		}
	}
}
