using System;
using WindowStream.Models;

namespace WindowStream.Services.Interfaces
{
	public interface IStreamStateService
	{
		StreamWindowInstance CreateInstance(IntPtr hwnd);
		void DeleteInstance(Guid id);
		StreamWindowInstance GetInstance(Guid id);
	}
}