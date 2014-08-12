using System;
using System.ComponentModel.Composition;
using ThinkingHome.Core.Plugins;
using ThinkingHome.NooLite;
using ThinkingHome.NooLite.ReceivedData;
using ThinkingHome.Plugins.Scripts;

namespace ThinkingHome.Plugins.NooLite
{
	[Plugin]
	public class NooLitePlugin : Plugin
	{
		private readonly RX1164Adapter rx1164 = new RX1164Adapter();
		private readonly RX2164Adapter rx2164 = new RX2164Adapter();

		public override void InitPlugin()
		{
			rx1164.CommandReceived += rx_CommandReceived;
			rx2164.CommandReceived += rx_CommandReceived;
		}

		void rx_CommandReceived(ReceivedCommandData obj)
		{
			Run(OnCommandReceivedForPlugins, x => x(obj.Cmd, obj.Channel, obj.Data));

			this.RaiseScriptEvent(x => x.OnCommandReceivedForScripts, obj.Cmd, obj.Channel, obj.Data);
		}

		public override void StartPlugin()
		{
			rx1164.OpenDevice();
			rx2164.OpenDevice();
		}

		public override void StopPlugin()
		{
			rx1164.Dispose();
			rx2164.Dispose();
		}

		[ScriptEvent("noolite.commandReceived")]
		public ScriptEventHandlerDelegate[] OnCommandReceivedForScripts { get; set; }

		[ImportMany("EB6000DD-79F1-408A-9325-4DCFFB1AD391")]
		public Action<int, int, byte[]>[] OnCommandReceivedForPlugins { get; set; }
		
		[ScriptCommand("nooliteSetLevel")]
		public void SetLevel(int channel, int level)
		{
			//Debugger.Launch();
			try
			{
				using (var adapter = new PC11XXAdapter())
				{
					if (adapter.OpenDevice())
					{
						adapter.SendCommand(PC11XXCommand.SetLevel, (byte)channel, (byte)level);
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
