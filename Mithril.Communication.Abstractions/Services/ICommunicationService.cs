using Mithril.Communication.Abstractions.Interfaces;

namespace Mithril.Communication.Abstractions.Services
{
    /// <summary>
    /// Communication service
    /// </summary>
    public interface ICommunicationService
    {
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
        /// <returns>The async task.</returns>
        Task SendMessageAsync(IMessage message);
    }
}