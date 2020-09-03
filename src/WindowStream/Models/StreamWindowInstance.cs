using System;

namespace WindowStream.Models
{
	public class StreamWindowInstance
	{
		private const int DefaultTimeout = 100;

		public Guid Id { get; } = Guid.NewGuid();

		public IntPtr Hwnd { get; }

		public int TimeoutInMilliseconds { get; set; } = DefaultTimeout;

		public long LastFrameRenderTime { get; set; } = 1;

		public TrimMargin TrimMargin { get; } = new TrimMargin();

		public StreamWindowInstance(IntPtr hwnd)
		{
			Hwnd = hwnd;
		}
	}
}
