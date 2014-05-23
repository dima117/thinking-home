using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using ThinkingHome.Core.Plugins;
using ThinkingHome.NooLite;
using ThinkingHome.NooLite.Common;
using ThinkingHome.Plugins.Scripts;

namespace ThinkingHome.Plugins.NooLite
{
	[Plugin]
	public class NooLitePlugin : Plugin
	{
		private readonly RX1164Adapter rx1164 = new RX1164Adapter();

		public override void Init()
		{
			rx1164.CommandReceived += rx1164_CommandReceived;
		}

		void rx1164_CommandReceived(ReceivedCommandData obj)
		{
			Run(OnCommandReceivedForPlugins, x => x(obj.Cmd, obj.Channel, obj.Data));

			this.RaiseScriptEvent(x => x.OnCommandReceivedForScripts, obj.Cmd, obj.Channel, obj.Data);
		}

		public override void Start()
		{
			rx1164.OpenDevice();
		}

		public override void Stop()
		{
			rx1164.Dispose();
		}

		[ScriptEvent("noolite.commandReceived")]
		public ScriptEventHandlerDelegate[] OnCommandReceivedForScripts { get; set; }

		[ImportMany("EB6000DD-79F1-408A-9325-4DCFFB1AD391")]
		public Action<int, int, byte[]>[] OnCommandReceivedForPlugins { get; set; }
		
		[ScriptCommand("noolite", "setLevel")]
		public void SetLevel(int channel, int level)
		{
			//Debugger.Launch();
			SetLevel((byte)channel, (byte)level);
		}

		public void SetLevel(byte channel, byte level)
		{
			try
			{
				using (var adapter = new PC11XXAdapter())
				{
					if (adapter.OpenDevice())
					{
						adapter.SendCommand(PC11XXCommand.SetLevel, channel, level);
						Logger.Info("set level {0} in channel {1}", level, channel);
					}
					else
					{
						Logger.Error("Can not connect to the device");
					}
				}
			}
			catch (Exception ex)
			{
				Logger.ErrorException(ex.Message, ex);
			}
		}
	}
}
