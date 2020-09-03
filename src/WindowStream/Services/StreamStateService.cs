using System;
using System.Collections.Concurrent;
using WindowStream.Models;
using WindowStream.Services.Interfaces;

namespace WindowStream.Services
{
	public class StreamStateService : IStreamStateService
	{
		private ConcurrentDictionary<Guid, StreamWindowInstance> _streamWindowInstances = new ConcurrentDictionary<Guid, StreamWindowInstance>();

		public StreamWindowInstance GetInstance(Guid id)
		{
			if (!_streamWindowInstances.TryGetValue(id, out var instance))
				return null;

			return instance;
		}

		public StreamWindowInstance CreateInstance(IntPtr hwnd)
		{
			var instance = new StreamWindowInstance(hwnd);
			if (!_streamWindowInstances.TryAdd(instance.Id, instance))
				throw new Exception("Tried to add the same stream window instance multiple times.");

			return instance;
		}

		public void DeleteInstance(Guid id)
		{
			_streamWindowInstances.TryRemove(id, out _);
		}
	}
}
