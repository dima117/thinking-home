using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkingHome.Plugins.Weather.Api
{
	public class DailyWeatherDataModel
	{
		public string Date { get; set; }
		public string Time { get; set; }
		public int T { get; set; }
		public int P { get; set; }
		public int H { get; set; }
		public string Code { get; set; }
		public string Description { get; set; }
	}
}
