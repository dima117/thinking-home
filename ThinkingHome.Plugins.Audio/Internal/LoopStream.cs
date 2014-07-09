using NAudio.Wave;

namespace ThinkingHome.Plugins.Audio.Internal
{
	public interface ILoopStream
	{
		void Stop();
	}

	/// <summary>
	/// Stream for looping playback
	/// </summary>
	public class LoopStream : WaveStream, ILoopStream
	{
		private readonly WaveStream sourceStream;
		private bool stop = false;

		public LoopStream(WaveStream sourceStream, int loop = 0)
		{
			this.sourceStream = sourceStream;
			Loop = loop;
		}

		public int Loop { get; private set; }

		public void Stop()
		{
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

			while (totalBytesRead < count)
			{
				if (stop)
				{
					return 0;
				}

				int bytesRead = sourceStream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);

				if (bytesRead == 0)
				{
					if (sourceStream.Position == 0 || (Loop--) <= 0)
					{
						// something wrong with the source stream
						break;
					}
					// loop

					sourceStream.Position = 0;
				}

				totalBytesRead += bytesRead;
			}

			return totalBytesRead;
		}
	}
}
