using System;
using ThinkingHome.Plugins.Scripts.Data;

namespace ThinkingHome.Plugins.Speech.Data
{
	public class VoiceCommand
	{
		public virtual Guid Id { get; set; }

		public virtual string CommandText { get; set; }

		public virtual UserScript UserScript { get; set; }
	}
}
