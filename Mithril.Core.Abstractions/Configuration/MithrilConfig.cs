﻿namespace Mithril.Core.Abstractions.Configuration
{
    /// <summary>
    /// Mithril config
    /// </summary>
    public class MithrilConfig
    {
        /// <summary>
        /// Gets or sets the compression.
        /// </summary>
        /// <value>The compression.</value>
        public Compression? Compression { get; set; }

        /// <summary>
        /// Gets or sets the file mappings.
        /// </summary>
        /// <value>The file mappings.</value>
        public List<Mime>? MimeTypes { get; set; } = new List<Mime>();

        /// <summary>
        /// Gets or sets the security.
        /// </summary>
        /// <value>The security.</value>
        public Security? Security { get; set; }

        /// <summary>
        /// Gets or sets the static files.
        /// </summary>
        /// <value>The static files.</value>
        public StaticFiles? StaticFiles { get; set; }
    }
}