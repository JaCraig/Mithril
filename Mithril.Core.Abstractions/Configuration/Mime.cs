namespace Mithril.Core.Abstractions.Configuration
{
    /// <summary>
    /// Mime type data holder
    /// </summary>
    public class Mime
    {
        /// <summary>
        /// Gets or sets the extension.
        /// </summary>
        /// <value>The extension.</value>
        public string? Extension { get; set; }

        /// <summary>
        /// Gets or sets the type of the MIME.
        /// </summary>
        /// <value>The type of the MIME.</value>
        public string? MimeType { get; set; }
    }
}