using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.OneWire.Attributes;

namespace ThinkingHome.Plugins.OneWire
{
	[Plugin]
	public class OneWirePluggin : PluginBase
	{
		private OneWireAdapter _adapter;
        private Type[] _oneWireSensorTypes;

		public override void InitPlugin()
		{
			//Debugger.Launch();
			base.InitPlugin();

            _adapter = new OneWireAdapter(OneWireAdapterConfiguration.Default);

            _oneWireSensorTypes = GetSensorsTypes();
		}

		public override void StartPlugin()
		{
			Logger.Info("Start Plugin OneWire");
			base.StartPlugin();
		}

		public override void StopPlugin()
		{
			base.StopPlugin();

			_adapter.Dispose();
			_adapter = null;
		}

		public OneWireDevice[] GetDevices()
		{
			// 1. получаем список устройств через адаптер
			// 2. парсим типы устройств, для неизвестных создаем экземпляр базового класса

            var listAddress = _adapter.FindAddress();

            List<OneWireDevice> list = new List<OneWireDevice>();

            foreach (var addr in listAddress)
            {
                foreach (var sensorType in _oneWireSensorTypes)
                {
                    var attr = sensorType.GetCustomAttribute<SensorTypeAttribute>(true);
                    if (attr != null && attr.Match(addr[0]))
                    {
                        var device = Activator.CreateInstance(sensorType, new object[] { addr, _adapter }) as OneWireDevice;
                        list.Add(device);
                    }
                }
            }

            return list.ToArray();
		}

        private Type[] GetSensorsTypes()
        {
            List<Type> listTypes = new List<Type>();

            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var t in a.GetTypes())
                {
                    if (t.IsSubclassOf(typeof(OneWireDevice)))
                    {
                        listTypes.Add(t);
                    }
                }
            }
            
            return listTypes.ToArray();
        }
	}
}
