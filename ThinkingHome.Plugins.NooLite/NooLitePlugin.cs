using System;
using System.ComponentModel.Composition;
using ThinkingHome.Core.Plugins;
using ThinkingHome.NooLite;
using ThinkingHome.NooLite.ReceivedData;
using ThinkingHome.Plugins.Scripts;

namespace ThinkingHome.Plugins.NooLite
{
	[Plugin]
	public class NooLitePlugin : PluginBase
	{
		private readonly RX1164Adapter rx1164 = new RX1164Adapter();
		private readonly RX2164Adapter rx2164 = new RX2164Adapter();

		public override void InitPlugin()
		{
			rx1164.CommandReceived += rx_CommandReceived;
			rx2164.CommandReceived += rx_CommandReceived;
			rx2164.MicroclimateDataReceived += rx2164_MicroclimateDataReceived;
		}

		void rx2164_MicroclimateDataReceived(MicroclimateReceivedCommandData obj)
		{
			Run(OnMicroclimateDataReceivedForPlugins, x => x(obj.Channel, obj.Temperature, obj.Humidity));

			this.RaiseScriptEvent(x => x.OnMicroclimateDataReceivedForScripts, obj.Channel, obj.Temperature, obj.Humidity);
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

		[ScriptEvent("noolite.microclimateDataReceived")]
		public ScriptEventHandlerDelegate[] OnMicroclimateDataReceivedForScripts { get; set; }

		[ImportMany("A40F6290-A9C5-439B-9CDF-32656F8D1C65")]
		public Action<int, decimal, int>[] OnMicroclimateDataReceivedForPlugins { get; set; }

		[ScriptCommand("nooliteSetLevel")]
		public void SetLevel(int channel, int level)
		{
			SendCommand((int)PC11XXCommand.SetLevel, channel, level);
		}

		[ScriptCommand("nooliteSendCommand")]
		public void SendCommand(int command, int channel, int level)
		{
			//Debugger.Launch();
			try
			{
				using (var adapter = new PC11XXAdapter())
				{
					if (adapter.OpenDevice())
					{
						var pc11XxCommand = (PC11XXCommand)command;
						adapter.SendCommand(pc11XxCommand, (byte)channel, (byte)level);
						Logger.Info("send command {0}: level {1} in channel {2}", pc11XxCommand, level, channel);
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

        [ScriptCommand("nooliteSendLedCommand")]
        public void SendLedCommand(int ledCommand, int channel, int levelR = 0, int levelG = 0, int levelB = 0)
        {
            //Debugger.Launch();
            try
            {
                using (var adapter = new PC11XXAdapter())
                {
                    if (adapter.OpenDevice())
                    {
                        var pc11XxLedCommand = (PC11XXLedCommand)ledCommand;
                        adapter.SendLedCommand(pc11XxLedCommand, (byte)channel, (byte)levelR, (byte)levelG, (byte)levelB);
                        Logger.Info("send command {0}: levelR {1} levelG {2} levelB{3} in channel {4}", pc11XxLedCommand, levelR, levelG, levelB, channel);
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
