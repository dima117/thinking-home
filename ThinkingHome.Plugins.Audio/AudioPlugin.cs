using System.IO;
using NAudio.Wave;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Audio.Internal;

namespace ThinkingHome.Plugins.Audio
{
	[Plugin]
	public class AudioPlugin : PluginBase
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

		public IPlayback Play(Stream stream, int loop = 1)
		{
			lock (lockObject)
			{
				var reader = new WaveFileReader(stream);
				var loopStream = new LoopStream(Logger, reader, loop);
				
				waveOut.Init(loopStream);
				waveOut.Play();

				return loopStream;
			}
		}
	}
}
