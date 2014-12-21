using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkingHome.Core.Plugins;

namespace ThinkingHome.Plugins.OneWire
{
	[Plugin]
	public class OneWirePluggin : PluginBase
	{
		private OneWireAdapter adapter;

		public override void InitPlugin()
		{
			Debugger.Launch();
			base.InitPlugin();

			adapter = new OneWireAdapter();
		}

		public override void StartPlugin()
		{
			Logger.Info("Start Plugin OneWire");
			base.StartPlugin();
		}

		public override void StopPlugin()
		{
			base.StopPlugin();

			adapter.Dispose();
			adapter = null;
		}

		public OneWireDevice[] GetDevices()
		{
			// 1. получаем список устройств через адаптер
			// 2. парсим типы устройств, для неизвестных создаем экземпляр базового класса

			throw new NotImplementedException();
		}

		public TDevice GetDevice<TDevice>(string address) where TDevice : OneWireDevice, new()
		{
			return new TDevice { Address = address, Adapter = adapter };
		}
	}
}
