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
        /// Gets the type of the view model it accepts.
        /// </summary>
        /// <value>The type of the view model it accepts.</value>
        Type ViewModelType { get; }

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
        /// <returns>A command value converted from the view model.</returns>
        ICommand? Create(TViewModel? value);
    }
}