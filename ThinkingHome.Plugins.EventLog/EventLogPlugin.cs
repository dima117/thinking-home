using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.ByCode;
using ThinkingHome.Core.Plugins;

namespace ThinkingHome.Plugins.EventLog
{
	[Plugin]
	public class EventLogPlugin : PluginBase
    {
		// todo: заменить AssemblyInfo
		public override void InitDbModel(ModelMapper mapper)
		{
			base.InitDbModel(mapper);
		}
    }
}
