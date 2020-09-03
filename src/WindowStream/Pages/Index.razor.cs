using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using WindowStream.Models;
using WindowStream.Services.Interfaces;

namespace WindowStream.Pages
{
	public class IndexBase : ComponentBase
	{
		[Inject]
		public IStreamStateService StreamStateService { get; set; }

		[Inject]
		public IJSRuntime JSRuntime { get; set; }

		protected StreamWindowInstance StreamWindowInstance { get; set; }

		protected void SelectWindow(IntPtr hwnd)
		{
			StreamWindowInstance = StreamStateService.CreateInstance(hwnd);
		}

		protected void ChooseNewWindow()
		{
			if (StreamWindowInstance != null)
				StreamStateService.DeleteInstance(StreamWindowInstance.Id);

			StreamWindowInstance = null;
			StateHasChanged();
		}

		protected void ApplyWindowsTrim()
		{
			const int borderTrim = 8;
			const int topTrim = 32;

			StreamWindowInstance.TrimMargin.Left = borderTrim;
			StreamWindowInstance.TrimMargin.Top = topTrim;
			StreamWindowInstance.TrimMargin.Right = borderTrim;
			StreamWindowInstance.TrimMargin.Bottom = borderTrim;

			StateHasChanged();
		}
	}
}
