using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using WindowStream.Helpers;
using WindowStream.Models;
using WindowStream.Services.Interfaces;

namespace WindowStream.Services
{
	public class CaptureWindowService : ICaptureWindowService
	{
		private byte[] GetImage(IntPtr hwnd, TrimMargin trimMargin)
		{
			var rect = new Native.Rect();
			Native.GetWindowRect(hwnd, ref rect);

			byte[] bytes = new byte[0];

			if (rect.Left == 0 && rect.Top == 0 && rect.Right == 0 && rect.Bottom == 0)
				return null;

			trimMargin ??= new TrimMargin();

			var width = rect.Right - rect.Left - trimMargin.Left - trimMargin.Right;
			var height = rect.Bottom - rect.Top - trimMargin.Top - trimMargin.Bottom;

			if (width < 0 || height < 0)
			{
				return null;
			}

			using (var memoryStream = new MemoryStream())
			using (var bitmap = new Bitmap(width, height))
			using (var graphic = Graphics.FromImage(bitmap))
			{
				graphic.CompositingQuality = CompositingQuality.HighSpeed;
				graphic.CompositingMode = CompositingMode.SourceCopy;
				graphic.CopyFromScreen(rect.Left + trimMargin.Left, rect.Top + trimMargin.Top, 0, 0, bitmap.Size, CopyPixelOperation.SourceCopy);

				bitmap.Save(memoryStream, ImageFormat.Jpeg);

				bytes = new byte[memoryStream.Length];
				memoryStream.Seek(0, SeekOrigin.Begin);
				memoryStream.Read(bytes, 0, bytes.Length);
			}

			GC.Collect();
			GC.WaitForPendingFinalizers();

			return bytes;
		}

		public Task<byte[]> GetImageAsync(IntPtr hwnd, TrimMargin trimMargin = null) =>
			Task.Run(() => GetImage(hwnd, trimMargin));
	}
}
