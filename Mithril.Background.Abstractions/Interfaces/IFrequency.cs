namespace Mithril.Background.Abstractions.Interfaces
{
    /// <summary>
    /// Frequency interface
    /// </summary>
    public interface IFrequency
    {
        /// <summary>
        /// Determines whether this instance can run based on the specified last run time.
        /// </summary>
        /// <param name="lastRunTime">The last run time.</param>
        /// <param name="currentTime">The current time.</param>
        /// <returns>
        ///   <c>true</c> if this instance can run the specified last run time; otherwise, <c>false</c>.
        /// </returns>
        bool CanRun(DateTime lastRunTime, DateTime currentTime);
    }
}