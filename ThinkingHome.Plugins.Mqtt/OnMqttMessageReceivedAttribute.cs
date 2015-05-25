using System;
using System.ComponentModel.Composition;

namespace ThinkingHome.Plugins.Mqtt
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class OnMqttMessageReceivedAttribute : ExportAttribute
	{
		public OnMqttMessageReceivedAttribute()
			: base("25DD679C-BB4E-449A-BFB8-42CC877CC32C", typeof(Action<MqttMessage>))
		{  
		}
	}
}