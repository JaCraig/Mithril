using Microsoft.Extensions.Logging;

namespace Mithril.Logging.Commands.ViewModels
{
    /// <summary>
    /// Log Command, used for reporting javascript related logging messages.
    /// </summary>
    public class LogCommandVM
    {
        /// <summary>
        /// The log level to use for the message.
        /// </summary>
        /// <value>The log level.</value>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// The message.
        /// </summary>
        /// <value>The message.</value>
        public string? Message { get; set; }
    }
}