using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WindowStream.Helpers
{
	public class Native
	{
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr FindWindow(string strClassName, string strWindowName);

		[DllImport("user32.dll")]
		public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

		public delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);

		[DllImport("USER32.DLL")]
		public static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

		[DllImport("USER32.DLL")]
		public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		[DllImport("USER32.DLL")]
		public static extern int GetWindowTextLength(IntPtr hWnd);

		[DllImport("USER32.DLL")]
		public static extern bool IsWindowVisible(IntPtr hWnd);

		[DllImport("USER32.DLL")]
		public static extern IntPtr GetShellWindow();

		public struct Rect
		{
			public int Left { get; set; }
			public int Top { get; set; }
			public int Right { get; set; }
			public int Bottom { get; set; }
		}
	}
}
