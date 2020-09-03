using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WindowStream.Helpers;
using WindowStream.Services.Interfaces;

namespace WindowStream.Pages
{
	public class StreamWindowModel : PageModel
	{
		private readonly ICaptureWindowService _captureWindowService;
		private readonly IStreamStateService _streamStateService;

		public StreamWindowModel(
			ICaptureWindowService captureWindowService,
			IStreamStateService streamStateService)
		{
			_captureWindowService = captureWindowService;
			_streamStateService = streamStateService;
		}

		public IActionResult OnGet(Guid id)
		{
			var streamInstance = _streamStateService.GetInstance(id);

			var isFirst = true;
			return new MjpegStreamContent(async cancellationToken =>
			{
				if (isFirst)
					isFirst = false; // no delay for first image
				else
					await Task.Delay(streamInstance.TimeoutInMilliseconds, cancellationToken);
				
				var stopwatch = new Stopwatch();
				stopwatch.Start();

				var bytes = await _captureWindowService.GetImageAsync(streamInstance.Hwnd, streamInstance.TrimMargin);

				stopwatch.Stop();
				streamInstance.LastFrameRenderTime = stopwatch.ElapsedMilliseconds;

				return bytes;
			},
			() => { });
		}
	}
}
