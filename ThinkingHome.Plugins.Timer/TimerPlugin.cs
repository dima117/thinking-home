using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Timers;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Timer.Attributes;
using ThinkingHome.Plugins.Timer.Internal;

namespace ThinkingHome.Plugins.Timer
{
	[Plugin]
	public class TimerPlugin : PluginBase
	{
		private const int TIMER_INTERVAL = 30000;
		private System.Timers.Timer timer;

		#region handlers

		[ImportMany("E62C804C-B96B-4CA8-822E-B1725B363534")]
		public Action<DateTime>[] OnEvent { get; set; }

		[ImportMany("38A9F1A7-63A4-4688-8089-31F4ED4A9A61")]
		public Lazy<Action, IRunPeriodicallyAttribute>[] PeriodicalActions { get; set; }

		private readonly List<PeriodicalActionState> periodicalHandlers = new List<PeriodicalActionState>();

		#endregion

		public override void InitPlugin()
		{
			timer = new System.Timers.Timer(TIMER_INTERVAL);
			timer.Elapsed += OnTimedEvent;

			RegisterPeriodicalHandlers();
		}

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

			// periodical actions
			foreach (var handler in periodicalHandlers)
			{
				handler.TryToExecute(now);
			}

			// timer event
			Run(OnEvent, x => x(now));
		}


		#region periodical handlers

		private void RegisterPeriodicalHandlers()
		{
			var now = DateTime.Now;

			Logger.Info("register periodical actions at {0:yyyy.MM.dd, HH:mm:ss}", now);

			foreach (var action in PeriodicalActions)
			{
				var handler = new PeriodicalActionState(action.Value, action.Metadata.Interval, now, Logger);

				periodicalHandlers.Add(handler);
			}
		}

		#endregion
	}
}
