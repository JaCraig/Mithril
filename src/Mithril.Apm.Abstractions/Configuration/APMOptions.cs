namespace Mithril.Apm.Abstractions.Configuration
{
    /// <summary>
    /// APM Options
    /// </summary>
    public class APMOptions
    {
        /// <summary>
        /// Gets or sets the batching frequency (in seconds).
        /// </summary>
        /// <value>The batching frequency (in seconds).</value>
        public int? BatchingFrequency { get; set; }

        /// <summary>
        /// Gets or sets the maximum age (in hours).
        /// </summary>
        /// <value>The maximum age (in hours).</value>
        public int? MaximumAge { get; set; }
    }
}