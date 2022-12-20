using Mithril.Communication.Abstractions.Interfaces;
using Mithril.Communication.Abstractions.Services;

namespace Mithril.Communication.Services
{
    /// <summary>
    /// Communication service
    /// </summary>
    /// <seealso cref="ICommunicationService"/>
    public class CommunicationService : ICommunicationService
    {
        public Task<IMessageTemplate> LoadOrCreateTemplateAsync(string displayName) => throw new NotImplementedException();

        public Task SendMessageAsync(IMessage message) => throw new NotImplementedException();
    }
}