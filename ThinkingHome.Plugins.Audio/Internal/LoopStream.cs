using NAudio.Wave;
using NLog;

namespace ThinkingHome.Plugins.Audio.Internal
{
	/// <summary>
	/// Stream for looping playback
	/// </summary>
	public class LoopStream : WaveStream, IPlayback
	{
		private readonly Logger log;
		private readonly WaveStream sourceStream;
		private bool stop;

		public LoopStream(Logger log, WaveStream sourceStream, int loop = 1)
		{
			this.log = log;
			this.sourceStream = sourceStream;
			Loop = loop;
		}

		public int Loop { get; private set; }

		public void Stop()
		{
			log.Debug("stop sound request");
			stop = true;
		}

		public override WaveFormat WaveFormat
		{
			get { return sourceStream.WaveFormat; }
		}

		public override long Length
		{
			get { return sourceStream.Length; }
		}

		public override long Position
		{
			get { return sourceStream.Position; }
			set { sourceStream.Position = value; }
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			int totalBytesRead = 0;

			while (totalBytesRead < count && Loop > 0)
			{
				if (stop)
				{
					log.Debug("stop sound");
					return 0;
				}

				int bytesRead = sourceStream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);
				
				if (bytesRead == 0)
				{
					if (sourceStream.Position == 0 && Loop <= 0)
					{
						log.Debug("end of file");
						break;
					}

					// loop
					Loop--;
					sourceStream.Position = 0;
				}

				totalBytesRead += bytesRead;
			}

			return totalBytesRead;
		}
	}
}
