using System;
using System.Threading;

namespace TimerInForm
{
	class SafeTimerDecorator : IDisposable
	{
		private readonly Timer _timer;

		/// <inheritdoc cref="Timer"/>
		public SafeTimerDecorator(TimerCallback callback, object state, TimeSpan dueTime, TimeSpan period)
		{
			_timer = new Timer(RunSafe(callback), state, dueTime, period);
		}

		public bool Change(TimeSpan dueTime, TimeSpan period)
		{
			return _timer.Change(dueTime, period);
		}

		public void Stop()
		{
			Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
		}

		private TimerCallback RunSafe(TimerCallback action)
		{
			return state =>
			{
				try
				{
					action(state);
				}
				catch (Exception ex)
				{
					//Do logging
				}
			};
		}

		public void Dispose()
		{
			_timer.Dispose();
		}
	}
}
