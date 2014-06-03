using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Media;
using NHibernate.Linq;
using NHibernate.Mapping.ByCode;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Core.Plugins.Utils;
using ThinkingHome.Plugins.AlarmClock.Data;
using ThinkingHome.Plugins.Listener;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Handlers;
using ThinkingHome.Plugins.Scripts;
using ThinkingHome.Plugins.Scripts.Data;
using ThinkingHome.Plugins.Timer;
using ThinkingHome.Plugins.WebUI.Attributes;

namespace ThinkingHome.Plugins.AlarmClock
{
	// list
	[AppSection("Alarms", SectionType.Common, "/webapp/alarm-clock/list.js", "ThinkingHome.Plugins.AlarmClock.Resources.alarm-list.js")]
	[JavaScriptResource("/webapp/alarm-clock/list-model.js", "ThinkingHome.Plugins.AlarmClock.Resources.alarm-list-model.js")]
	[JavaScriptResource("/webapp/alarm-clock/list-view.js", "ThinkingHome.Plugins.AlarmClock.Resources.alarm-list-view.js")]
	[HttpResource("/webapp/alarm-clock/list.tpl", "ThinkingHome.Plugins.AlarmClock.Resources.alarm-list.tpl")]
	[HttpResource("/webapp/alarm-clock/list-item.tpl", "ThinkingHome.Plugins.AlarmClock.Resources.alarm-list-item.tpl")]

	// editor
	[JavaScriptResource("/webapp/alarm-clock/editor.js", "ThinkingHome.Plugins.AlarmClock.Resources.alarm-editor.js")]
	[JavaScriptResource("/webapp/alarm-clock/editor-model.js", "ThinkingHome.Plugins.AlarmClock.Resources.alarm-editor-model.js")]
	[JavaScriptResource("/webapp/alarm-clock/editor-view.js", "ThinkingHome.Plugins.AlarmClock.Resources.alarm-editor-view.js")]
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
			player = new SoundPlayer(SoundResources.Ring02);
		}

		public override void Stop()
		{
			player.Dispose();
		}

		#region events

		[ImportMany("0917789F-A980-4224-B43F-A820DEE093C8")]
		public Action<Guid>[] AlarmStartedForPlugins { get; set; }

		[ScriptEvent("alarmClock.alarmStarted")]
		public ScriptEventHandlerDelegate[] AlarmStartedForScripts { get; set; }

		#endregion

		#region private

		[OnTimerElapsed]
		private void OnTimerElapsed(DateTime now)
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

		private static bool CheckTime(AlarmTime time, DateTime now, DateTime lastAlarm)
		{
			// если прошло время звонка будильника
			// и от этого времени не прошло 5 минут
			// и будильник сегодня еще не звонил
			var date = now.Date.AddHours(time.Hours).AddMinutes(time.Minutes);

			if (date < lastAlarm)
			{
				date = date.AddDays(1);
			}

			return now > date && now < date.AddMinutes(5) && lastAlarm < date;
		}

		private void Alarm(AlarmTime[] alarms)
		{
			player.PlayLooping();

			foreach (var alarm in alarms)
			{
				Logger.Info("ALARM: {0} ({1})", alarm.Name, alarm.Id);

				Guid alarmId = alarm.Id;
				Run(AlarmStartedForPlugins, x => x(alarmId));
			}

			this.RaiseScriptEvent(x => x.AlarmStartedForScripts);
		}

		private void StopAlarm()
		{
			player.Stop();
		}

		#endregion

		#region api list

		[HttpCommand("/api/alarm-clock/list")]
		public object GetAlarmList(HttpRequestParams request)
		{
			using (var session = Context.OpenSession())
			{
				var list = session.Query<AlarmTime>().ToList();

				var model = list
					.Select(alarm => new
						{
							id = alarm.Id,
							name = alarm.Name,
							hours = alarm.Hours,
							minutes = alarm.Minutes,
							enabled = alarm.Enabled,
							scriptId = alarm.UserScript.GetValueOrDefault(obj => (Guid?)obj.Id),
							scriptName = alarm.UserScript.GetValueOrDefault(obj => obj.Name)
						})
					.ToArray();

				return model;
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

		[HttpCommand("/api/alarm-clock/delete")]
		public object DeleteAlarm(HttpRequestParams request)
		{
			var id = request.GetRequiredGuid("id");

			using (var session = Context.OpenSession())
			{
				var alarmTime = session.Load<AlarmTime>(id);
				session.Delete(alarmTime);
				session.Flush();
			}

			ReloadTimes();
			return null;
		}

		[HttpCommand("/api/alarm-clock/stop")]
		public object StopAlarm(HttpRequestParams request)
		{
			StopAlarm();
			return null;
		}

		#endregion

		#region api list

		[HttpCommand("/api/alarm-clock/save")]
		public object SaveAlarm(HttpRequestParams request)
		{
			var id = request.GetGuid("id");
			var name = request.GetString("name");
			var hours = request.GetRequiredInt32("hours");
			var minutes = request.GetRequiredInt32("minutes");
			var scriptId = request.GetGuid("scriptId");

			using (var session = Context.OpenSession())
			{
				var alarmTime = id.HasValue
					? session.Get<AlarmTime>(id.Value)
					: new AlarmTime { Id = Guid.NewGuid() };

				var script = scriptId.HasValue
					? session.Load<UserScript>(scriptId.Value)
					: null;

				alarmTime.Name = name;
				alarmTime.Hours = hours;
				alarmTime.Minutes = minutes;
				alarmTime.UserScript = script;
				alarmTime.Enabled = true;

				session.Save(alarmTime);
				session.Flush();
			}

			ReloadTimes();
			return null;
		}

		[HttpCommand("/api/alarm-clock/editor")]
		public object LoadEditor(HttpRequestParams request)
		{
			var id = request.GetGuid("id");

			using (var session = Context.OpenSession())
			{
				var scriptList = session
					.Query<UserScript>()
					.Select(s => new { id = s.Id, name = s.Name })
					.ToArray();

				if (id.HasValue)
				{
					var alarm = session.Get<AlarmTime>(id.Value);

					return BuildEditorModel(
						scriptList,
						alarm.Id,
						alarm.Name,
						alarm.Hours,
						alarm.Minutes,
						alarm.Enabled,
						alarm.UserScript.GetValueOrDefault(obj => (Guid?)obj.Id)
					);
				}

				return BuildEditorModel(scriptList);
			}
		}

		private static object BuildEditorModel(
			object scripts, Guid? id = null, string name = null, int hours = 0, int minutes = 0, bool enabled = false, Guid? scriptId = null)
		{
			return new { id, name, hours, minutes, enabled, scriptId, scripts };
		}


		#endregion
	}
}
