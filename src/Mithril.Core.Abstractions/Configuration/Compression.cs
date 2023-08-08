namespace Mithril.Core.Abstractions.Configuration
{
    /// <summary>
    /// Compression settings
    /// </summary>
    public class Compression
    {
        /// <summary>
        /// Gets or sets a value indicating whether [allow HTTPS].
        /// </summary>
        /// <value><c>true</c> if [allow HTTPS]; otherwise, <c>false</c>.</value>
        public bool AllowHttps { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [dynamic compression].
        /// </summary>
        /// <value><c>true</c> if [dynamic compression]; otherwise, <c>false</c>.</value>
        public bool DynamicCompression { get; set; }
    }
}