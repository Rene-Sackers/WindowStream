﻿/// Thanks to Chris Green :)
/// Source: https://blog.green.web.za/2019/11/23/mjpeg-in-asp-net-core.html

using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowStream.Helpers
{
	public class MjpegStreamContent : IActionResult
	{
		private static readonly string _boundary = "MotionImageStream";
		private static readonly string _contentType = "multipart/x-mixed-replace;boundary=" + _boundary;
		private static readonly byte[] _newLine = Encoding.UTF8.GetBytes("\r\n");

		private readonly Func<CancellationToken, Task<byte[]>> _onNextImage;
		private readonly Action _onEnd;

		public MjpegStreamContent(Func<CancellationToken, Task<byte[]>> onNextImage, Action onEnd)
		{
			_onNextImage = onNextImage;
			_onEnd = onEnd;
		}

		public async Task ExecuteResultAsync(ActionContext context)
		{
			context.HttpContext.Response.ContentType = _contentType;

			var outputStream = context.HttpContext.Response.Body;
			var cancellationToken = context.HttpContext.RequestAborted;

			try
			{
				while (true)
				{
					var imageBytes = await _onNextImage(cancellationToken);
					if (imageBytes == null)
						break;

					var header = $"--{_boundary}\r\nContent-Type: image/jpeg\r\nContent-Length: {imageBytes.Length}\r\n\r\n";
					var headerData = Encoding.UTF8.GetBytes(header);
					await outputStream.WriteAsync(headerData, 0, headerData.Length, cancellationToken);
					await outputStream.WriteAsync(imageBytes, 0, imageBytes.Length, cancellationToken);
					await outputStream.WriteAsync(_newLine, 0, _newLine.Length, cancellationToken);

					if (cancellationToken.IsCancellationRequested)
						break;
				}
			}
			catch (TaskCanceledException)
			{
				// connection closed, no need to report this
			}
			finally
			{
				_onEnd();
			}
		}
	}
}
