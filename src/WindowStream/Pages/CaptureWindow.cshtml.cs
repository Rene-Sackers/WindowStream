using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using WindowStream.Services.Interfaces;

namespace WindowStream.Pages
{
	public class CaptureWindowModel : PageModel
	{
		private readonly ICaptureWindowService _captureWindowService;

		public CaptureWindowModel(ICaptureWindowService captureWindowService)
		{
			_captureWindowService = captureWindowService;
		}

		public async Task<IActionResult> OnGetAsync(int hwnd)
		{
			var bytes = await _captureWindowService.GetImageAsync(new IntPtr(hwnd));
			if (bytes == null)
				return BadRequest();

			return File(bytes, "image/jpeg");
		}
	}
}
