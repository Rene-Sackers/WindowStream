using System;
using System.Collections.Generic;
using System.Text;

namespace WindowStream.Helpers
{
	/// <summary>Contains functionality to get all the open windows.</summary>
	public static class OpenWindowsHelper
	{
		private const int SizeCutoff = 30;

		/// <summary>Returns a dictionary that contains the handle and title of all the open windows.</summary>
		/// <returns>A dictionary that contains the handle and title of all the open windows.</returns>
		public static IDictionary<IntPtr, string> GetOpenWindows()
		{
			IntPtr shellWindow = Native.GetShellWindow();
			Dictionary<IntPtr, string> windows = new Dictionary<IntPtr, string>();

			Native.EnumWindows(delegate (IntPtr hWnd, int lParam)
			{
				if (hWnd == shellWindow) return true;
				if (!Native.IsWindowVisible(hWnd)) return true;

				var size = new Native.Rect();
				Native.GetWindowRect(hWnd, ref size);

				var width = size.Right - size.Left;
				var height = size.Bottom - size.Top;
				if ((width <= SizeCutoff) || (height <= SizeCutoff))
					return true;

				int length = Native.GetWindowTextLength(hWnd);
				if (length == 0)
				{
					windows[hWnd] = "-no title-";
					return true;
				}

				StringBuilder builder = new StringBuilder(length);
				Native.GetWindowText(hWnd, builder, length + 1);

				windows[hWnd] = builder.ToString();
				return true;

			}, 0);

			return windows;
		}
	}
}
