using System;
using System.Threading.Tasks;
using NLog;

namespace ThinkingHome.Plugins.Timer
{
	public class PeriodicalActionState
	{
		private DateTime lastRun;
		private readonly Logger logger;

		private readonly object lockObject = new object(); 
		private readonly Action<DateTime> action;
		private readonly int interval;

		public PeriodicalActionState(Action<DateTime> action, int interval, DateTime lastRun, Logger logger)
		{
			this.action = action;
			this.interval = interval;
			this.lastRun = lastRun;
			this.logger = logger;
		}

		public void TryToExecute(DateTime now)
		{
			if (lastRun.AddMinutes(interval) < now)
			{
				lock (lockObject)
				{
					if (lastRun.AddMinutes(interval) < now)
					{
						lastRun = now;

						string taskInfo = string.Format(
							"{0}, {1} at {2}", action.Method, action.Method.DeclaringType, now);
						
						Task.Run(() =>
						{
							try
							{
								logger.Info("run periodical task {0}", taskInfo);
								action(now);
								logger.Info("task is completed: {0}", taskInfo);
							}
							catch (Exception ex)
							{
								var msg = string.Format("error when running task {0}", taskInfo);
								logger.ErrorException(msg, ex);
							}
						});
					}
				}
			}
		}
	}
}
