using System.Configuration;
using System.IO;
using System.Net;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Scripts;

namespace ThinkingHome.Plugins.DlinkDCS930L
{
	[Plugin]
	public class DlinkDCS930LPlugin : Plugin
	{
		[ScriptCommand("dcs930", "getImage")]
		public byte[] GetImage(string ip = null)
		{
			var cameraIp = ip ?? ConfigurationManager.AppSettings["testCameraIP"];
			var bytes = GetImage(cameraIp, "admin", "");

			return bytes;
		}

		protected static byte[] GetImage(string ip, string login, string password)
		{
			string url = string.Format(@"http://{0}/image/jpeg.cgi", ip);
			WebRequest request = WebRequest.Create(url);
			request.Credentials = new NetworkCredential(login, password);
			request.PreAuthenticate = true;

			using (var response = request.GetResponse())
			{
				using (var stream = response.GetResponseStream())
				{
					if (stream == null)
					{
						return new byte[0];
					}

					var ms = new MemoryStream();
					stream.CopyTo(ms);
					byte[] data = ms.ToArray();
					return data;
				}
			}
		}
	}
}
