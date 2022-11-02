using System.Dynamic;

namespace Mithril.API.Abstractions.Commands.Interfaces
{
    /// <summary>
    /// Command handler interface
    /// </summary>
    public interface ICommandHandler
    {
        /// <summary>
        /// Gets the command type accepted.
        /// </summary>
        /// <value>The command type accepted.</value>
        Type CommandTypeAccepted { get; }

        /// <summary>
        /// Creates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A command value converted from the ExpandoObject.</returns>
        ICommand? Create(ExpandoObject value);

        /// <summary>
        /// Handles the Command.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <returns>Any events that are spawned by the command.</returns>
        ICommandEvent[] HandleCommand(params ICommand[] arg);
    }
}