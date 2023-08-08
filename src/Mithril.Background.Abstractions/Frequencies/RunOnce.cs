using Mithril.Background.Abstractions.Interfaces;

namespace Mithril.Background.Abstractions.Frequencies
{
    /// <summary>
    /// Run once
    /// </summary>
    /// <seealso cref="IFrequency" />
    public class RunOnce : IFrequency
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance has run.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has run; otherwise, <c>false</c>.
        /// </value>
        private bool HasRun { get; set; }

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
            if (!HasRun)
            {
                HasRun = true;
                return true;
            }
            return false;
        }
    }
}