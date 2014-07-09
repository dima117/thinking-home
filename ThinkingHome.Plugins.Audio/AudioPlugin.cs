using System.IO;
using NAudio.Wave;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Audio.Internal;

namespace ThinkingHome.Plugins.Audio
{
	[Plugin]
	public class AudioPlugin : Plugin
	{
		private WaveOut waveOut;
		private readonly object lockObject = new object();

		public override void InitPlugin()
		{
			waveOut = new WaveOut();
		}

		public override void StopPlugin()
		{
			waveOut.Dispose();
			waveOut = null;
		}

		public ILoopStream Play(Stream stream, int loop = 0)
		{
			lock (lockObject)
			{
				var reader = new WaveFileReader(stream);
				var loopStream = new LoopStream(reader, loop);
				
				waveOut.Init(loopStream);
				waveOut.Play();

				return loopStream;
			}
		}

		public void StopAllSound()
		{
			lock (lockObject)
			{
				waveOut.Stop();
			}
		}
	}
}
