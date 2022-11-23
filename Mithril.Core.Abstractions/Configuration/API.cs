namespace Mithril.Core.Abstractions.Configuration
{
    /// <summary>
    /// API configuration information
    /// </summary>
    public class API
    {
        /// <summary>
        /// Gets or sets the allow anonymous users access (defaults to false).
        /// </summary>
        /// <value>The allow anonymous users access.</value>
        public bool? AllowAnonymous { get; set; }

        /// <summary>
        /// Gets or sets the authorization policy (if empty then just Authorize called).
        /// </summary>
        /// <value>The authorization policy.</value>
        public string? AuthorizationPolicy { get; set; }

        /// <summary>
        /// Gets or sets the command endpoint.
        /// </summary>
        /// <value>The command endpoint.</value>
        public string? CommandEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the command run frequency (in seconds). If set to 0, then command
        /// processing will not run.
        /// </summary>
        /// <value>The command run frequency (in seconds).</value>
        public int? CommandRunFrequency { get; set; }

        /// <summary>
        /// Gets or sets the event run frequency (in seconds). If set to 0, then event processing
        /// will not run.
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
        /// Gets or sets the open API endpoint.
        /// </summary>
        /// <value>The open API endpoint.</value>
        public string? OpenAPIEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the query endpoint.
        /// </summary>
        /// <value>The query endpoint.</value>
        public string? QueryEndpoint { get; set; }
    }
}