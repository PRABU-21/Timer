
using System;
using System.Diagnostics;

namespace TaskTimerApp.Services
{
    public class TimerService
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public void Start()
        {
            if (!_stopwatch.IsRunning)
            {
                _stopwatch.Start();
            }
        }

        public TimeSpan GetElapsedTime()
        {
            return _stopwatch.Elapsed;
        }

        public TimeSpan Stop()
        {
            _stopwatch.Stop();

            TimeSpan elapsed = _stopwatch.Elapsed;

            _stopwatch.Reset();

            return elapsed;
        }

        public bool IsRunning
        {
            get { return _stopwatch.IsRunning; }
        }
    }
}
