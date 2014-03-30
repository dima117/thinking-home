using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Speech.Synthesis;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Scripts;

namespace ThinkingHome.Plugins.Speech
{
	[Plugin]
	public class SpeechPlugin : Plugin
    {
		private SpeechSynthesizer speechSynthesizer;

		public override void Init()
		{
			base.Init();
			speechSynthesizer = new SpeechSynthesizer();
			speechSynthesizer.SetOutputToDefaultAudioDevice();
		}

		public override void Stop()
		{
			base.Stop();
			speechSynthesizer.Dispose();
		}

		#region methods

		[ScriptCommand("speech", "say")]
		public void Say(string text)
		{
			speechSynthesizer.Speak(text);
		}

		#endregion
    }
}
