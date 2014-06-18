using System;
using System.Linq;
using NHibernate.Linq;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Core.Plugins.Utils;
using ThinkingHome.Plugins.AlarmClock.Data;
using ThinkingHome.Plugins.Listener;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Handlers;
using ThinkingHome.Plugins.Scripts.Data;
using ThinkingHome.Plugins.WebUI.Attributes;
using ThinkingHome.Plugins.WebUI.Model;

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
	public class AlarmClockUiPlugin : Plugin
	{
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

			Context.GetPlugin<AlarmClockPlugin>().ReloadTimes();
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

			Context.GetPlugin<AlarmClockPlugin>().ReloadTimes();
			return null;
		}

		[HttpCommand("/api/alarm-clock/stop")]
		public object StopAlarm(HttpRequestParams request)
		{
			Context.GetPlugin<AlarmClockPlugin>().StopAlarm();
			return null;
		}

		#endregion

		#region api editor

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

			Context.GetPlugin<AlarmClockPlugin>().ReloadTimes();
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

		#region tiles

		[Tile("48AFCCC4-A3B1-41B3-B23A-2EA3DAFD6F55", "Alarm clock", "webapp/alarm-clock/list")]
		public void AlarmClockTile(TileModel tile)
		{
			var now = DateTime.Now;
			var times = Context
				.GetPlugin<AlarmClockPlugin>()
				.GetNextAlarmTimes(now)
				.Take(4);

			var strTimes = times.Select(t => t.ToShortTimeString()).ToArray();

			tile.content = strTimes.Any()
				? string.Join(Environment.NewLine, strTimes)
				: "There are no active alarms";
		}

		#endregion
	}
}
