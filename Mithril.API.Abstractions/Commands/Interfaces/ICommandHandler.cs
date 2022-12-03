using Mithril.Core.Abstractions.Modules.Interfaces;
using System.Security.Claims;

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
        string CommandName { get; }

        /// <summary>
        /// Gets the content type accepted by command handler.
        /// </summary>
        /// <value>The content type accepted by command handler.</value>
        string[] ContentTypeAccepts { get; }

        /// <summary>
        /// Gets the features associated with this command.
        /// </summary>
        /// <value>The features associated with this command.</value>
        IFeature[] Features { get; }

        /// <summary>
        /// Gets the tags (Used by OpenAPI, etc).
        /// </summary>
        /// <value>The tags (Used by OpenAPI, etc).</value>
        string[] Tags { get; }

        /// <summary>
        /// Gets the type of the view model it accepts.
        /// </summary>
        /// <value>The type of the view model it accepts.</value>
        Type ViewModelType { get; }

        /// <summary>
        /// Determines whether this instance can handle the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>
        /// <c>true</c> if this instance can handle the specified command; otherwise, <c>false</c>.
        /// </returns>
        bool CanHandle(ICommand command);

        /// <summary>
        /// Handles the Command.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <returns>Any events that are spawned by the command.</returns>
        IEvent[] HandleCommand(params ICommand[] arg);
    }

    /// <summary>
    /// Command handler interface
    /// </summary>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    public interface ICommandHandler<TViewModel> : ICommandHandler
    {
        /// <summary>
        /// Creates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="user">The user.</param>
        /// <returns>A command value converted from the view model.</returns>
        CommandCreationResult? Create(TViewModel? value, ClaimsPrincipal user);
    }
}