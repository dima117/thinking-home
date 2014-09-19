using System;
using System.Linq;
using ThinkingHome.Plugins.WebUI.Model;
using ThinkingHome.Plugins.WebUI.Tiles;

namespace ThinkingHome.Plugins.AlarmClock
{
	[Tile]
	public class AlarmClockTileDefinition : TileDefinition
	{
		public override void FillModel(TileModel model, dynamic options)
		{
			model.title = "Alarm clock";
			model.url = "webapp/alarm-clock/list";
			model.content = GetAlarmTileContent();
			model.className = "btn-primary th-tile-icon th-tile-icon-fa fa-bell";
		}

		private string GetAlarmTileContent()
		{
			var now = DateTime.Now;
			var times = Context
				.GetPlugin<AlarmClockPlugin>()
				.GetNextAlarmTimes(now)
				.Take(4);

			var strTimes = times.Select(t => t.ToShortTimeString()).ToArray();

			var content = strTimes.Any()
				? string.Join(Environment.NewLine, strTimes)
				: "There are no active alarms";
			return content;
		}
	}
}
