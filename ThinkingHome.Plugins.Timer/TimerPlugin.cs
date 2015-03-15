using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Timers;
using ThinkingHome.Core.Plugins;

namespace ThinkingHome.Plugins.Timer
{
	[Plugin]
	public class TimerPlugin : PluginBase
	{
		private static readonly Random random = new Random();

		private const int TIMER_INTERVAL = 30000;

		private System.Timers.Timer timer;
		private readonly List<PeriodicalActionState> periodicalHandlers = new List<PeriodicalActionState>();

		#region handlers

		[ImportMany("E62C804C-B96B-4CA8-822E-B1725B363534")]
		public Action<DateTime>[] OnEvent { get; set; }

		[ImportMany("38A9F1A7-63A4-4688-8089-31F4ED4A9A61")]
		public Lazy<Action<DateTime>, IRunPeriodicallyAttribute>[] PeriodicalActions { get; set; }

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

			TryToExecurePeriodicalHandlers(now);

			Run(OnEvent, x => x(now));
		}


		#region periodical handlers

		private void RegisterPeriodicalHandlers()
		{
			var now = DateTime.Now;

			Logger.Info("register periodical actions at {0:yyyy.MM.dd, HH:mm:ss}", now);

			foreach (var action in PeriodicalActions)
			{
				RegisterAction(action.Value, action.Metadata, now);
			}
		}

		private void RegisterAction(Action<DateTime> action, IRunPeriodicallyAttribute metadata, DateTime now)
		{
			Logger.Info("register periodical action: {0} ({1})", action.Method, action.Method.DeclaringType);

			if (metadata.Interval < 1)
			{
				string msg = string.Format("wrong interval: {0}", metadata.Interval);

				Logger.Error(msg);
				throw new Exception(msg);
			}

			int offset = random.Next(metadata.Interval);
			var lastRun = now.AddMinutes(offset - metadata.Interval);

			Logger.Info("interval: {0} minutes, random offset: {1} minutes", metadata.Interval, offset);

			var handler = new PeriodicalActionState(action, metadata.Interval, lastRun);
			periodicalHandlers.Add(handler);
		}

		private void TryToExecurePeriodicalHandlers(DateTime now)
		{
			foreach (var handler in periodicalHandlers)
			{
				// todo: продумать обработку исключений и многопоточность
				handler.TryToExecute(now);	
			}
		}

		#endregion
	}
}
