namespace Mithril.Core.Abstractions.Configuration
{
    /// <summary>
    /// Security options
    /// </summary>
    public class Security
    {
        /// <summary>
        /// Gets or sets the content security policy.
        /// </summary>
        /// <value>The content security policy.</value>
        public string? ContentSecurityPolicy { get; set; }

        /// <summary>
        /// Gets or sets the default cors origins (if empty, CORS is not enabled by default).
        /// </summary>
        /// <value>The default cors origins (if empty, CORS is not enabled by default).</value>
        public string? DefaultCorsPolicy { get; set; }

        /// <summary>
        /// Gets or sets the require HTTPS.
        /// </summary>
        /// <value>The require HTTPS.</value>
        public bool RequireHttps { get; set; }

        /// <summary>
        /// Gets or sets the x-frame options.
        /// </summary>
        /// <value>The x-frame options.</value>
        public string? XFrameOptions { get; set; }
    }
}