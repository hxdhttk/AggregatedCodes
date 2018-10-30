using System.Collections.Concurrent;
using System.Timers;

public class AutomaticClearConcurrentBag<T> : ConcurrentBag<T>
{
    private Timer _clearTimer;

    private Func<Task> _clearFunction;

    public AutomaticClearConcurrentBag (int capacityLimit, int checkInterval, Func<Task> clearFunction) : base ()
    {
        _clearFunction = clearFunction;
        _clearTimer = new Timer (checkInterval);
        _clearTimer.Elapsed += async (_, __) =>
        {
            _clearTimer.Stop ();

            if (this.Count >= capacityLimit)
            {
                await clearFunction ();
            }

            _clearTimer.Start ();
        };

        _clearTimer.Start ();
    }

    public async Task ForceClear()
    {
        _clearTimer.Stop();
        await _clearFunction();
        _clearTimer.Start();
    }
}