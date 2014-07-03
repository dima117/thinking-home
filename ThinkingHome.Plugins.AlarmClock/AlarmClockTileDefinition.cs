using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkingHome.Plugins.WebUI.Tiles;

namespace ThinkingHome.Plugins.AlarmClock
{
	[Tile("48AFCCC4-A3B1-41B3-B23A-2EA3DAFD6F55", "Alarm clock", "webapp/alarm-clock/list")]
	public class AlarmClockTileDefinition : TileDefinition
	{
		public override void FillModel(WebUI.Model.TileModel model)
		{
			var now = DateTime.Now;
			var times = Context
				.GetPlugin<AlarmClockPlugin>()
				.GetNextAlarmTimes(now)
				.Take(4);

			var strTimes = times.Select(t => t.ToShortTimeString()).ToArray();

			model.content = strTimes.Any()
				? string.Join(Environment.NewLine, strTimes)
				: "There are no active alarms";

		}
	}
}
