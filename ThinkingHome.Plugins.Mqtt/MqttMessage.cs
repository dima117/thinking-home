using System;
using System.Text;

namespace ThinkingHome.Plugins.Mqtt
{
	public class MqttMessage
	{
		public string path;
		public DateTime timestamp;
		public byte[] message;

		public string GetUtf8String()
		{
			return Encoding.UTF8.GetString(message ?? new byte[0]);
		}

		public string GetBase64String()
		{
			return Encoding.UTF8.GetString(message ?? new byte[0]);
		}
	}
}