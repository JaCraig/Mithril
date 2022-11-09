namespace Mithril.Core.Abstractions.Configuration
{
    /// <summary>
    /// API configuration information
    /// </summary>
    public class API
    {
        /// <summary>
        /// Gets or sets the command run frequency (in seconds).
        /// </summary>
        /// <value>The command run frequency (in seconds).</value>
        public int? CommandRunFrequency { get; set; }

        /// <summary>
        /// Gets or sets the event run frequency (in seconds).
        /// </summary>
        /// <value>The event run frequency (in seconds).</value>
        public int? EventRunFrequency { get; set; }

        /// <summary>
        /// Gets or sets the maximum command processing time (in ms).
        /// </summary>
        /// <value>The maximum command processing time (in ms).</value>
        public int? MaxCommandProcessTime { get; set; }

        /// <summary>
        /// Gets or sets the maximum event processing time (in ms).
        /// </summary>
        /// <value>The maximum event processing time (in ms).</value>
        public int? MaxEventProcessTime { get; set; }

        /// <summary>
        /// Gets or sets the query endpoint.
        /// </summary>
        /// <value>The query endpoint.</value>
        public string? QueryEndpoint { get; set; }
    }
}