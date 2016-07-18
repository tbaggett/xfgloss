using System;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace XFGlossSample.Utils
{
	internal delegate void TimerCallback(object state);

	internal sealed class Timer : CancellationTokenSource, IDisposable
	{
		internal Timer(TimerCallback callback, object state, int dueTime, int period)
		{
			Contract.Assert(period == -1, "This stub implementation only supports dueTime.");
			Task.Delay(dueTime, Token).ContinueWith((t, s) =>
				{
					var tuple = (Tuple<TimerCallback, object>)s;
					tuple.Item1(tuple.Item2);
				}, Tuple.Create(callback, state), CancellationToken.None,
													TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnRanToCompletion,
													TaskScheduler.Default);
		}

		public new void Dispose() { Cancel(); }
	}
}