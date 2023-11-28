using System;
using System.Threading;

namespace Common
{
	public static class UIThread
	{
		private static SynchronizationContext _UIThread;

		public static void Init() {
			_UIThread = SynchronizationContext.Current;
		}

		public static SynchronizationContext GetUIThread() {
			return _UIThread;
		}

		public static void Post(Action action) {
			if(action != null){
				_UIThread.Post(delegate (object state)
				               {
				               	action.Invoke();
				               }, null);
			}
		}

		public static void Async(Action action, Action<Exception> done)
		{
			try
			{
				action();
			}catch(Exception e)
			{
				if(done != null)
				{
					done(e);
				}
			}
		}
	}
}
