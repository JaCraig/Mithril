using Microsoft.Extensions.Logging;

namespace Mithril.Logging.Commands
{
    /// <summary>
    /// Log Command VM
    /// </summary>
    public class LogCommandVM
    {
        /// <summary>
        /// Gets or sets the log level.
        /// </summary>
        /// <value>The log level.</value>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string? Message { get; set; }
    }
}