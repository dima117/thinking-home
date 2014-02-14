using System;
using System.ComponentModel.Composition;
using System.Timers;
using ThinkingHome.Core.Plugins;

namespace ThinkingHome.Plugins.Timer
{
	[Plugin]
	public class TimerPlugin : Plugin
	{
		private const int TIMER_INTERVAL = 30000;

		private System.Timers.Timer timer;

		public override void Init()
		{
			timer = new System.Timers.Timer(TIMER_INTERVAL);
			timer.Elapsed += OnTimedEvent;
		}

		[ImportMany("E62C804C-B96B-4CA8-822E-B1725B363534")]
		public Action<DateTime>[] OnEvent { get; set; }

		//[OnTimerElapsed]
		//public void OnTimerElapsed(DateTime now)
		//{
		//	DateTime lastAlarmTime = DateTime.MinValue;
		//	var alarmTime = now.Date.AddHours(8).AddMinutes(15);	// ставим будильник на сегодня на 8:15
			
		//	if (now > alarmTime && 									// если пришло время звонка будильника
		//		now < alarmTime.AddMinutes(5) && 					// и от этого времени не прошло 5 минут
		//		lastAlarmTime < alarmTime)							// и будильник сегодня еще не звонил
		//	{
		//		lastAlarmTime = now;
		//		Context.GetPlugin<SoundPlugin>().PlayAlarmSound();
		//	}
		//}

		public override void Start()
		{
			timer.Enabled = true;
		}

		public override void Stop()
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
