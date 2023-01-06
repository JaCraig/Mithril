using Mithril.Communication.Abstractions.Interfaces;
using System.Security.Claims;

namespace Mithril.Communication.Abstractions.Services
{
    /// <summary>
    /// Communication service
    /// </summary>
    public interface ICommunicationService
    {
        /// <summary>
        /// Creates a message based on the channel specified.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <returns>The message object.</returns>
        IMessage? CreateMessage(string channel);

        /// <summary>
        /// Loads or creates a template asynchronously.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <returns>The message template.</returns>
        Task<IMessageTemplate> LoadOrCreateTemplateAsync(string displayName);

        /// <summary>
        /// Sends the message asynchronously.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="user">The user.</param>
        /// <returns>The message result.</returns>
        Task<MessageResult> SendMessageAsync(IMessage? message, ClaimsPrincipal? user);
    }
}