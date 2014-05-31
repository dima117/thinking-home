using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using NHibernate.Linq;
using NHibernate.Mapping.ByCode;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.AlarmClock.Data;
using ThinkingHome.Plugins.Listener;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Handlers;
using ThinkingHome.Plugins.Timer;
using ThinkingHome.Plugins.WebUI.Attributes;

namespace ThinkingHome.Plugins.AlarmClock
{

	[AppSection("Alarms", SectionType.Common, "/webapp/alarm-clock/list.js", "ThinkingHome.Plugins.AlarmClock.Resources.alarm-list.js")]
	[JavaScriptResource("/webapp/alarm-clock/list-model.js", "ThinkingHome.Plugins.AlarmClock.Resources.alarm-list-model.js")]
	[JavaScriptResource("/webapp/alarm-clock/list-view.js", "ThinkingHome.Plugins.AlarmClock.Resources.alarm-list-view.js")]
	[HttpResource("/webapp/alarm-clock/list.tpl", "ThinkingHome.Plugins.AlarmClock.Resources.alarm-list.tpl")]
	[HttpResource("/webapp/alarm-clock/list-item.tpl", "ThinkingHome.Plugins.AlarmClock.Resources.alarm-list-item.tpl")]
	[HttpResource("/webapp/alarm-clock/editor.tpl", "ThinkingHome.Plugins.AlarmClock.Resources.alarm-editor.tpl")]

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

		[OnTimerElapsed]
		public void OnTimerElapsed(DateTime now)
		{
			lock (lockObject)
			{
				LoadTimes();

				if (CheckTime(now))
				{
					lastAlarmTime = now;
					Alarm();
				}
			}
		}

		#region private

		private void ReloadTimes()
		{
			lock (lockObject)
			{
				times = null;
				LoadTimes();
			}
		}

		private void LoadTimes()
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

		private void Alarm()
		{
			Logger.Info("ALARM! ALARM! ALARM!");
		}

		#endregion

		#region api

		[HttpCommand("/api/alarm-clock/list")]
		public object GetAlarmList(HttpRequestParams request)
		{
			using (var session = Context.OpenSession())
			{
				var list = session.Query<AlarmTime>()
					.Select(x => new
					{
						id = x.Id,
						name = x.Name,
						hours = x.Hours,
						minutes = x.Minutes,
						enabled = x.Enabled
					})
					.ToArray();

				return list;
			}
		}

		[HttpCommand("/api/alarm-clock/set-state")]
		public object SetAlarmState(HttpRequestParams request)
		{
			var id = request.GetRequiredGuid("id");
			var enabled = request.GetRequiredBool("enabled");
			
			using (var session = Context.OpenSession())
			{
				var alarmTime = session.Get<AlarmTime>(id);
				alarmTime.Enabled = enabled;

				session.Save(alarmTime);
				session.Flush();
			}

			ReloadTimes();
			return null;
		}

		[HttpCommand("/api/alarm-clock/save")]
		public object SaveAlarm(HttpRequestParams request)
		{
			var id = request.GetGuid("id");
			var name = request.GetString("name");
			var hours = request.GetRequiredInt32("hours");
			var minutes = request.GetRequiredInt32("minutes");
			
			using (var session = Context.OpenSession())
			{
				var alarmTime = id.HasValue
					? session.Get<AlarmTime>(id.Value)
					: new AlarmTime();

				alarmTime.Hours = hours;
				alarmTime.Minutes = minutes;
				alarmTime.Name = name;
				alarmTime.Enabled = true;

				session.Save(alarmTime);
				session.Flush();
			}

			ReloadTimes();
			return null;
		}


		#endregion
	}
}
