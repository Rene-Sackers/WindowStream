using System;
using System.Threading.Tasks;
using WindowStream.Models;

namespace WindowStream.Services.Interfaces
{
	public interface ICaptureWindowService
	{
		Task<byte[]> GetImageAsync(IntPtr hwnd, TrimMargin trimMargin = null);
	}
}