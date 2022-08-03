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
        /// Gets or sets the x-frame options.
        /// </summary>
        /// <value>The x-frame options.</value>
        public string? XFrameOptions { get; set; }
    }
}