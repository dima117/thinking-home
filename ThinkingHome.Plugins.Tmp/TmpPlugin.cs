using System;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Threading;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.DlinkDCS930L;
using ThinkingHome.Plugins.Scripts;

namespace ThinkingHome.Plugins.Tmp
{
	[Plugin]
    public class TmpPlugin : Plugin
    {
		#region camera
		[ScriptCommand("dcs930", "sendImage")]
		public void SendImage(string to, string subject)
		{
			var bytes = Context.GetPlugin<DlinkDCS930LPlugin>().GetImage();

			var fileName = string.Format("{0:yyyyMMddHHmmss}.jpg", DateTime.Now);
			var path = Path.Combine(@"D:\", fileName);
			File.WriteAllBytes(path, bytes);

			Send(to, subject, fileName, bytes);
		}
		
		#endregion

		#region mail

		public void Send(string to, string subject, string fileName, byte[] bytes)
		{
			var message = new MailMessage
			{
				To = { to },
				Subject = subject
			};

			if (bytes != null)
			{
				var attachment = new Attachment(new MemoryStream(bytes), fileName);
				message.Attachments.Add(attachment);
			}

			ThreadPool.QueueUserWorkItem(state =>
			{
				try
				{
					SendEmailAwaitable(message);
				}
				catch (Exception ex)
				{
					Logger.ErrorException("mail exception", ex);
				}
			});
		}

		private void SendEmailAwaitable(MailMessage message)
		{
			using (var smtpClient = new SmtpClient())
			{
				smtpClient.Send(message);
			}
		}

		#endregion

		#region sounds

		private SoundPlayer doorBell;

		public override void Start()
		{
			doorBell = new SoundPlayer(TmpResources.doorbell);
		}

		public override void Stop()
		{
			doorBell.Dispose();
		}

		[ScriptCommand("sounds", "doorBell")]
		public void DoorBell()
		{
			doorBell.Play();
		}

		#endregion
	}
}
