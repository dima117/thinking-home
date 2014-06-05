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
using ThinkingHome.Core.Plugins.Utils;
using ThinkingHome.Plugins.Scripts;
using ThinkingHome.Plugins.Speech.Data;

namespace ThinkingHome.Plugins.Speech
{
	[Plugin]
	public class SpeechPlugin : Plugin
	{
		private SpeechSynthesizer speechSynthesizer;
		private SpeechRecognitionEngine recognitionEngine;

		private DateTime? readyDate;
		private const string NAME = "эй кампьютэр";
		private const string RESPONSE_READY = "слушаю!";
		private const int READY_PERIOD = 15;
		private const float CONFIDENCE_LIMIT = 0.5f;

		#region override

		public override void Init()
		{
			base.Init();

			InitSpeechSynthesizer();
			InitRecognitionEngine();
		}

		public override void Stop()
		{
			base.Stop();

			CloseSpeechSynthesizer();
			CloseRecognitionEngine();
		}

		#endregion

		#region synthesizer

		private void InitSpeechSynthesizer()
		{
			speechSynthesizer = new SpeechSynthesizer();
			speechSynthesizer.SetOutputToDefaultAudioDevice();
		}

		private void CloseSpeechSynthesizer()
		{
			speechSynthesizer.Dispose();
		}

		#endregion

		#region recognition engine

		private void InitRecognitionEngine()
		{
			var cultureInfo = CultureInfo.GetCultureInfo("ru-RU");
			Thread.CurrentThread.CurrentCulture = cultureInfo;
			Thread.CurrentThread.CurrentUICulture = cultureInfo;

			recognitionEngine = new SpeechRecognitionEngine(cultureInfo);
			recognitionEngine.SetInputToDefaultAudioDevice();

			var commands = LoadAllCommands();
			var choices = new Choices(commands);
			var builder = new GrammarBuilder(choices);


			recognitionEngine.LoadGrammar(new Grammar(builder));

			recognitionEngine.SpeechRecognized += OnSpeechRecognized;
			recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
		}

		private void CloseRecognitionEngine()
		{
			if (recognitionEngine != null)
			{
				recognitionEngine.Dispose();
			}
		}
	
		#endregion

		#region data

		public override void InitDbModel(ModelMapper mapper)
		{
			mapper.Class<VoiceCommand>(cfg => cfg.Table("Speech_VoiceCommand"));
		}

		private string[] LoadAllCommands()
		{
			using (var session = Context.OpenSession())
			{
				List<string> list = session
					.Query<VoiceCommand>()
					.Select(cmd => cmd.CommandText)
					.ToList();

				Logger.Info("loaded commands: {0}", list.ToJson("[]"));

				list.Add(NAME);
				
				return list.ToArray();
			}
		}

		private VoiceCommand GetCommand(string text)
		{
			using (var session = Context.OpenSession())
			{
				var command = session.Query<VoiceCommand>().FirstOrDefault(x => x.CommandText == text);

				if (command != null)
				{
					Logger.Info("loaded command: {0} (script: {1})", command.CommandText, command.UserScript.Name);
				}

				return command;
			}
		}

		#endregion

		#region events

		private void OnSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
		{
			var commandText = e.Result.Text;
			Logger.Info("command: {0} ({1:0.00})", commandText, e.Result.Confidence);

			if (e.Result.Confidence < CONFIDENCE_LIMIT)
			{
				Logger.Info("apply confidence limit");
				return;
			}

			var now = DateTime.Now;
			var isInPeriod = readyDate.GetValueOrDefault() > now;

			if (commandText == NAME)
			{
				Logger.Info("command is COMPUTER NAME");
				readyDate = now.AddSeconds(READY_PERIOD);
				Say(RESPONSE_READY);
			}
			else
			{
				if (isInPeriod)
				{
					try
					{
						//Debugger.Launch();
						var command = GetCommand(commandText);
						Logger.Info("command info loaded");

						Context.GetPlugin<ScriptsPlugin>().ExecuteScript(command.UserScript);

						this.RaiseScriptEvent(x => x.OnCommandReceivedForScripts, commandText);

						readyDate = null;
					}
					catch (Exception ex)
					{
						var msg = string.Format("voice command error: '{0}'", commandText);
						Logger.ErrorException(msg, ex);
					}

				}
			}
		}

		[ScriptEvent("speech.commandReceived")]
		public ScriptEventHandlerDelegate[] OnCommandReceivedForScripts { get; set; }


		#endregion

		#region methods

		[ScriptCommand("say")]
		public void Say(string text)
		{
			speechSynthesizer.Speak(text);
		}

		#endregion
	}
}
