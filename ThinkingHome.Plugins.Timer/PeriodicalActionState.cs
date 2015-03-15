using System;
using System.Threading.Tasks;

namespace ThinkingHome.Plugins.Timer
{
	public class PeriodicalActionState
	{
		private DateTime lastRun;

		private readonly Action<DateTime> action;
		private readonly int interval;

		public PeriodicalActionState(Action<DateTime> action, int interval, DateTime lastRun)
		{
			this.action = action;
			this.interval = interval;
			this.lastRun = lastRun;
		}

		public void TryToExecute(DateTime now)
		{
		}
	}
}
