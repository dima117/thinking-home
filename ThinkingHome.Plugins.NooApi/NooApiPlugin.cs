using System;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.NooLite;

namespace ThinkingHome.Plugins.NooApi
{
	[Plugin]
	class NooApiPlugin : PluginBase
	{
		private const int CMD_SET_BRIGHTNESS = 6;
		
		[HttpCommand("/api/noolite")]
		public object DispatchApiQuery(HttpRequestParams request)
		{
			int channel = request.GetRequiredInt32("ch");
			int cmd = request.GetRequiredInt32("cmd");

			switch (cmd)
			{
				// Set command - Установка яркости (формат в процентах и через аргументы d0, d1, d2)
				case CMD_SET_BRIGHTNESS:
					Logger.Debug("nooAPI Set command received, channel = {0}", channel);
					int? brightness = request.GetInt32("br");
					if (brightness != null)
					{
						int value = (155 * brightness.Value / 100);
						Context.GetPlugin<NooLitePlugin>().SendCommand(CMD_SET_BRIGHTNESS, channel, value);
					}
					else
					{
						int format = request.GetRequiredInt32("fmt");
						int d0 = request.GetRequiredInt32("d0");
						switch (format)
						{
							case 1:
								Context.GetPlugin<NooLitePlugin>().SendCommand(CMD_SET_BRIGHTNESS, channel, d0);
								break;

							case 3:
								int d1 = request.GetRequiredInt32("d1");
								int d2 = request.GetRequiredInt32("d2");
								Context.GetPlugin<NooLitePlugin>().SendLedCommand(CMD_SET_BRIGHTNESS, channel, d0, d1, d2);
								break;

							default:
								string message = string.Format("Insupported FMT value {0}", format);
								throw new NullReferenceException(message);
						}
					}
					break;

				default:
					Logger.Debug("nooAPI {0} command received, channel = {1}", cmd, channel);
					Context.GetPlugin<NooLitePlugin>().SendCommand(cmd, channel, 0);
					break;
			}
			return "OK";
		}
	}
}
