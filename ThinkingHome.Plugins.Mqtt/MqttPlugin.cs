using ECM7.Migrator.Framework;
using NHibernate.Mapping.ByCode;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Mqtt.Model;

[assembly: MigrationAssembly("ThinkingHome.Plugins.Mqtt")]

namespace ThinkingHome.Plugins.Mqtt
{
	[Plugin]
    public class MqttPlugin : PluginBase
    {
		public override void InitDbModel(ModelMapper mapper)
		{
			mapper.Class<Broker>(cfg => cfg.Table("Mqtt_Broker"));
			mapper.Class<Subscription>(cfg => cfg.Table("Mqtt_Subscription"));
		}
    }
}
