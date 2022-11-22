using Microsoft.AspNetCore.Http;
using Mithril.API.Abstractions.Commands.Interfaces;

namespace Mithril.API.Abstractions.Commands
{
    /// <summary>
    /// Command creation result.
    /// </summary>
    public class CommandCreationResult
    {
        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>The command.</value>
        public ICommand? Command { get; set; }

        /// <summary>
        /// Gets or sets the exception (used for logging).
        /// </summary>
        /// <value>The exception (used for logging).</value>
        public Exception? Exception { get; set; }

        /// <summary>
        /// Gets or sets the result text to send to caller.
        /// </summary>
        /// <value>The result text to send to caller.</value>
        public string? ResultText { get; set; } = "Success";

        /// <summary>
        /// Gets or sets the return code (if something other than 200, .
        /// </summary>
        /// <value>The return code.</value>
        public int ReturnCode { get; set; } = StatusCodes.Status200OK;
    }
}