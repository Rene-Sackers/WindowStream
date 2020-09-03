using System.Threading;
using System.Threading.Tasks;
using WindowStream.Helpers;

namespace WindowStream.Services
{
	public static class MouseClickHelper
	{
		public static async void Click(int x, int y)
		{
			Native.SetCursorPos(x, y);
			Native.mouse_event(Native.MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
			await Task.Delay(50);
			Native.mouse_event(Native.MOUSEEVENTF_LEFTUP, x, y, 0, 0);
		}
	}
}
