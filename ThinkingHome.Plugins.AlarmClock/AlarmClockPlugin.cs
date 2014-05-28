using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using NHibernate.Linq;
using NHibernate.Mapping.ByCode;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.AlarmClock.Data;
using ThinkingHome.Plugins.Timer;

namespace ThinkingHome.Plugins.AlarmClock
{
	[Plugin]
	public class AlarmClockPlugin : Plugin
	{
		private readonly object lockObject = new object();
		private DateTime lastAlarmTime = DateTime.MinValue;
		private List<AlarmTime> times;
		
		private SoundPlayer player;
		
		public override void InitDbModel(ModelMapper mapper)
		{
			mapper.Class<AlarmTime>(cfg => cfg.Table("AlarmClock_AlarmTime"));
		}

		public override void Start()
		{
			player = new SoundPlayer();
		}

		public override void Stop()
		{
			player.Dispose();
		}

		private void UpdateTimes()
		{
			if (times == null)
			{
				using (var session = Context.OpenSession())
				{
					times = session.Query<AlarmTime>().Where(t => t.Enabled).ToList();
					Logger.Info("loaded {0} alarm times", times.Count);
				}
			}
		}

		private static DateTime GetDate(AlarmTime time, DateTime now, DateTime lastAlarm)
		{
			var date = now.Date.AddHours(time.Hours).AddMinutes(time.Minutes);
			return date > lastAlarm ? date : date.AddDays(1);
		}

		[OnTimerElapsed]
		public void OnTimerElapsed(DateTime now)
		{
			lock (lockObject)
			{
				UpdateTimes();

				if (CheckTime(now))
				{
					lastAlarmTime = now;
					Alarm();
				}
			}
		}

		private void Alarm()
		{
			Logger.Info("ALARM! ALARM! ALARM!");
		}

		private bool CheckTime(DateTime now)
		{
			// если прошло время звонка будильника
			// и от этого времени не прошло 5 минут
			// и будильник сегодня еще не звонил

			var list = times
				.Select(t => GetDate(t, now, lastAlarmTime))
				.Where(t => now > t && now < t.AddMinutes(5) && lastAlarmTime < t)
				.ToArray();

			return list.Any();
		}
	}
}
