using Microsoft.JSInterop;
using System;
using WindowStream.Helpers;
using WindowStream.Services;

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

		[JSInvokable(nameof(PerformClickJs))]
		public void PerformClickJs(double relativeX, double relativeY)
		{
			var rect = new Native.Rect();
			Native.GetWindowRect(Hwnd, ref rect);

			var windowWidth = rect.Right - rect.Left;
			var windowHeight = rect.Bottom - rect.Top;
			var clickX = windowWidth * relativeX + TrimMargin.Left + rect.Left;
			var clickY = windowHeight * relativeY + TrimMargin.Top + rect.Top;

			MouseClickHelper.Click((int) clickX, (int) clickY);
		}

		public StreamWindowInstance(IntPtr hwnd)
		{
			Hwnd = hwnd;
		}
	}
}
