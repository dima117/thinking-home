using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.Synthesis;
using NHibernate.Linq;
using NHibernate.Mapping.ByCode;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Scripts;
using ThinkingHome.Plugins.Speech.Data;

namespace ThinkingHome.Plugins.Speech
{
	[Plugin]
	public class SpeechPlugin : Plugin
    {
		private SpeechSynthesizer speechSynthesizer;
		private SpeechRecognitionEngine recognitionEngine;

		private DateTime ready = DateTime.MinValue;
		private const string Name = "большая хрюката";

		public override void Init()
		{
			base.Init();
			speechSynthesizer = new SpeechSynthesizer();
			speechSynthesizer.SetOutputToDefaultAudioDevice();

			ReloadRecognitionEngine();
		}

		public override void Stop()
		{
			base.Stop();
			speechSynthesizer.Dispose();

			CloseRecognitionEngine();
		}

		private void CloseRecognitionEngine()
		{
			if (recognitionEngine != null)
			{
				recognitionEngine.Dispose();
			}
		}

		private void ReloadRecognitionEngine()
		{
			CloseRecognitionEngine();

			var cultureInfo = CultureInfo.GetCultureInfo("ru-RU");
			Thread.CurrentThread.CurrentCulture = cultureInfo;
			Thread.CurrentThread.CurrentUICulture = cultureInfo;

			recognitionEngine = new SpeechRecognitionEngine(cultureInfo);
			recognitionEngine.SetInputToDefaultAudioDevice();

			string[] commands;

			using (var session = Context.OpenSession())
			{
				List<string> list = session
					.Query<VoiceCommand>()
					.Select(cmd => cmd.CommandText)
					.ToList();
				
				list.Add(Name);
				commands = list.ToArray();
			}

			var choices = new Choices(commands);
			var builder = new GrammarBuilder(choices);


			recognitionEngine.LoadGrammar(new Grammar(builder));

			recognitionEngine.SpeechRecognized += OnSpeechRecognized;
			recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
		}

		private void OnSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
		{
			Logger.Info(e.Result.Text);
		}

		public override void InitDbModel(ModelMapper mapper)
		{
			mapper.Class<VoiceCommand>(cfg => cfg.Table("Speech_VoiceCommand"));
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
