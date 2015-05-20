using System;
using System.Threading.Tasks;
using NLog;

namespace ThinkingHome.Plugins.Timer.Internal
{
	public class PeriodicalActionState
	{
		private static readonly Random random = new Random();

		private DateTime lastRun;
		private readonly Logger logger;

		private readonly object lockObject = new object(); 
		private readonly Action action;
		private readonly int interval;

		public PeriodicalActionState(Action action, int interval, DateTime now, Logger logger)
		{
			logger.Info("register periodical action: {0} ({1})", action.Method, action.Method.DeclaringType);

			// validation
			if (interval < 1)
			{
				throw new Exception(string.Format("wrong interval: {0} min", interval));
			}

			// offset
			int offset = random.Next(interval);
			logger.Info("interval: {0} minutes, random offset: {1} minutes", interval, offset);

			// set fields
			this.interval = interval;
			this.action = action;
			this.logger = logger;

			lastRun = now.AddMinutes(offset - interval);
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
								action();
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
