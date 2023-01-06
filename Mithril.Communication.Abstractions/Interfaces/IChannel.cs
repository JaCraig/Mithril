namespace Mithril.Communication.Abstractions.Interfaces
{
    /// <summary>
    /// Communication channel
    /// </summary>
    public interface IChannel
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Determines whether this instance can handle the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        /// <c>true</c> if this instance can handle the specified message; otherwise, <c>false</c>.
        /// </returns>
        bool CanHandle(IMessage? message);

        /// <summary>
        /// Creates a message object.
        /// </summary>
        /// <returns>The message related to the channel.</returns>
        IMessage CreateMessage();

        /// <summary>
        /// Sends the message asynchronously.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The message result</returns>
        Task<MessageResult> SendMessageAsync(IMessage? message);
    }
}