using Mithril.Communication.Abstractions;
using Mithril.Communication.Abstractions.Commands;
using Mithril.Communication.Abstractions.Interfaces;
using Mithril.Communication.Abstractions.Services;
using Mithril.Communication.Models;
using Mithril.Data.Abstractions.Services;
using System.Security.Claims;

namespace Mithril.Communication.Services
{
    /// <summary>
    /// Communication service
    /// </summary>
    /// <seealso cref="ICommunicationService"/>
    public class CommunicationService : ICommunicationService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommunicationService"/> class.
        /// </summary>
        /// <param name="channels">The channels.</param>
        /// <param name="dataService">The data service.</param>
        public CommunicationService(IEnumerable<IChannel> channels, IDataService? dataService)
        {
            Channels = channels;
            DataService = dataService;
        }

        /// <summary>
        /// Gets the communication channels.
        /// </summary>
        /// <value>The communication channels.</value>
        private IEnumerable<IChannel> Channels { get; }

        /// <summary>
        /// Gets the data service.
        /// </summary>
        /// <value>The data service.</value>
        private IDataService? DataService { get; }

        /// <summary>
        /// Creates a message based on the channel specified.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <returns>The message object.</returns>
        public IMessage? CreateMessage(string channel)
        {
            return Channels.FirstOrDefault(x => string.Equals(x.Name, channel, StringComparison.OrdinalIgnoreCase))?.CreateMessage();
        }

        /// <summary>
        /// Loads or creates a template asynchronously.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <returns>The message template.</returns>
        public Task<IMessageTemplate> LoadOrCreateTemplateAsync(string displayName)
        {
            return MessageTemplate.LoadOrCreateAsync(DataService, displayName);
        }

        /// <summary>
        /// Sends the message asynchronously.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="user">The user.</param>
        /// <returns>The async task.</returns>
        public async Task<MessageResult> SendMessageAsync(IMessage? message, ClaimsPrincipal? user)
        {
            if (message is null)
            {
                return new MessageResult("Null message", new ArgumentNullException(nameof(message)));
            }
            if (Channels.FirstOrDefault(Channel => Channel.CanHandle(message)) is null)
            {
                return new MessageResult($"Channel that can handle {message?.GetType().Name ?? "NULL"} not found",
                                                        new ArgumentOutOfRangeException(nameof(message), $"Channel that can handle {message?.GetType().Name ?? "NULL"} not found"));
            }
            await message.SaveAsync(DataService, user).ConfigureAwait(false);
            await new SendMessageCommand(message).SaveAsync(DataService, user).ConfigureAwait(false);
            return new MessageResult("Message put into queue for sending.");
        }
    }
}