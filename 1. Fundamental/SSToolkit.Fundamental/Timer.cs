namespace SSToolkit.Fundamental
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;

    public class Timer : IDisposable
    {
        private readonly Stopwatch watch = new();

        public Timer()
        {
            this.watch.Start();
        }

        /// <summary>
        /// Gets the total elapsed time measured by the current instance, in milliseconds.
        /// </summary>
        public long ElapsedMilliseconds => this.watch.ElapsedMilliseconds;

        /// <summary>
        /// Gets the total elapsed time measured by the current instance, in timer ticks.
        /// </summary>
        public long ElapsedTicks => this.watch.ElapsedTicks;

        /// <summary>
        /// Stops time interval measurement, resets the elapsed time to zero, and starts measuring elapsed time.
        /// </summary>
        public void Restart()
        {
            this.watch.Restart();
        }

        /// <summary>
        ///  Stops time interval measurement and resets the elapsed time to zero.
        /// </summary>
        public void Reset()
        {
            this.watch.Reset();
        }

        /// <summary>
        /// Stops measuring elapsed time for an interval.
        /// </summary>
        public void Stop()
        {
            this.watch.Stop();
        }

        public void Dispose()
        {
            if (this.watch.IsRunning)
            {
                this.watch.Stop();
            }
        }
    }
}
