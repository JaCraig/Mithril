namespace Mithril.HealthChecks.Abstractions.Configuration
{
    /// <summary>
    /// Health checks config options
    /// </summary>
    public class MithrilHealthCheckOptions
    {
        /// <summary>
        /// Gets the check end point (defaults to '/api/healthchecks').
        /// </summary>
        /// <value>The check end point (defaults to '/api/healthchecks').</value>
        public string? CheckEndPoint { get; set; }

        /// <summary>
        /// Gets or sets the default timeout (in seconds).
        /// </summary>
        /// <value>The default timeout (in seconds).</value>
        public int? DefaultTimeout { get; set; }
    }
}