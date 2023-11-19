using Mithril.Background.Abstractions.Interfaces;

namespace Mithril.Background.Abstractions.Frequencies
{
    /// <summary>
    /// Run every X amount of time based on TimeSpan
    /// </summary>
    /// <seealso cref="IFrequency" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="RunEvery"/> class.
    /// </remarks>
    /// <param name="timeSpan">The time span.</param>
    public class RunEvery(TimeSpan timeSpan) : IFrequency
    {
        /// <summary>
        /// Gets the time span.
        /// </summary>
        /// <value>
        /// The time span.
        /// </value>
        private TimeSpan TimeSpan { get; } = timeSpan;

        /// <summary>
        /// Determines whether this instance can run based on the specified last run time.
        /// </summary>
        /// <param name="lastRunTime">The last run time.</param>
        /// <param name="currentTime">The current time.</param>
        /// <returns>
        ///   <c>true</c> if this instance can run the specified last run time; otherwise, <c>false</c>.
        /// </returns>
        public bool CanRun(DateTime lastRunTime, DateTime currentTime)
        {
            try
            {
                return lastRunTime.Add(TimeSpan) <= currentTime;
            }
            catch { return false; }
        }
    }
}