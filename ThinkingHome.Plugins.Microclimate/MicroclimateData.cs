using System;

namespace ThinkingHome.Plugins.Microclimate
{
	/// <summary>
	/// Модель для отправки данных на клиент
	/// </summary>
	public class MicroclimateData
	{
		public int channel;

		public int temperature;

		public int? humidity;

		public DateTime timestamp;
	}
}
