using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using NHibernate.Linq;
using NHibernate.Mapping.ByCode;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.AlarmClock.Data;
using ThinkingHome.Plugins.Audio;
using ThinkingHome.Plugins.Audio.Internal;
using ThinkingHome.Plugins.Scripts;
using ThinkingHome.Plugins.Timer.Attributes;
using ThinkingHome.Plugins.AlarmClock.Lang;

namespace ThinkingHome.Plugins.AlarmClock
{
	[Plugin]
	public class AlarmClockPlugin : PluginBase
	{
		private readonly object lockObject = new object();
		private readonly object lockObjectForSound = new object();
		private DateTime lastAlarmTime = DateTime.MinValue;
		private List<AlarmTime> times;
		private IPlayback playback;
	
		public override void InitDbModel(ModelMapper mapper)
		{
			mapper.Class<AlarmTime>(cfg => cfg.Table("AlarmClock_AlarmTime"));
		}

		#region public

		public void ReloadTimes()
		{
			lock (lockObject)
			{
				times = null;
				LoadTimes();
			}
		}

		public void PlaySound()
		{
			lock (lockObjectForSound)
			{
				StopSound();

				Logger.Info(AlarmClockLang.Play_sound);
				playback = Context.GetPlugin<AudioPlugin>().Play(SoundResources.Ring02, 25);
			}
		}

		public void StopSound()
		{
			lock (lockObjectForSound)
			{
				if (playback != null)
				{
					Logger.Info(AlarmClockLang.Stop_all_sounds);
					playback.Stop();
					playback = null;
				}
			}
		}

		#endregion

		#region events

		[ImportMany("0917789F-A980-4224-B43F-A820DEE093C8")]
		public Action<Guid>[] AlarmStartedForPlugins { get; set; }

		[ScriptEvent("alarmClock.alarmStarted")]
		public ScriptEventHandlerDelegate[] AlarmStartedForScripts { get; set; }

		#endregion

		#region private

		[OnTimerElapsed]
		public void OnTimerElapsed(DateTime now)
		{
			lock (lockObject)
			{
				LoadTimes();

				var alarms = times.Where(x => CheckTime(x, now, lastAlarmTime)).ToArray();

				if (alarms.Any())
				{
					lastAlarmTime = now;
					Alarm(alarms);
				}
			}
		}

		private void LoadTimes()
		{
			if (times == null)
			{
				using (var session = Context.OpenSession())
				{
					times = session.Query<AlarmTime>()
						.Fetch(a => a.UserScript)
						.Where(t => t.Enabled)
						.ToList();

					Logger.Info(AlarmClockLang.Loaded_0_alarm_times, times.Count);
				}
			}
		}

		public static DateTime GetDateTime(AlarmTime time, DateTime now, DateTime lastAlarm)
		{
			var date = now.Date.AddHours(time.Hours).AddMinutes(time.Minutes);

			if (date < lastAlarm || date.AddMinutes(5) < now )
			{
				date = date.AddDays(1);
			}

			return date;
		}

		private static bool CheckTime(AlarmTime time, DateTime now, DateTime lastAlarm)
		{
			// если прошло время звонка будильника
			// и от этого времени не прошло 5 минут
			// и будильник сегодня еще не звонил
			var date = GetDateTime(time, now, lastAlarm);

			return lastAlarm < date && date < now;
		}

		private void Alarm(AlarmTime[] alarms)
		{
			Logger.Info(AlarmClockLang.Alarm);

			if (alarms.Any(a => a.PlaySound))
			{
				PlaySound();
			}

			foreach (var alarm in alarms)
			{
				Logger.Info(AlarmClockLang.Run_event_handlers_0_1, alarm.Name, alarm.Id);

				Guid alarmId = alarm.Id;
				Run(AlarmStartedForPlugins, x => x(alarmId));

				if (alarm.UserScript != null)
				{
					Logger.Info(AlarmClockLang.Run_alarm_script_0_1, alarm.UserScript.Name, alarm.UserScript.Id);
					Context.GetPlugin<ScriptsPlugin>().ExecuteScript(alarm.UserScript);
				}
			}

			Logger.Info(AlarmClockLang.Run_subscribed_scripts);
			this.RaiseScriptEvent(x => x.AlarmStartedForScripts);
		}

		#endregion

		public DateTime[] GetNextAlarmTimes(DateTime now)
		{
			lock (lockObject)
			{
				LoadTimes();

				return times
					.Select(t => GetDateTime(t, now, lastAlarmTime))
					.OrderBy(t => t)
					.ToArray();
			}
		}
	}
}
