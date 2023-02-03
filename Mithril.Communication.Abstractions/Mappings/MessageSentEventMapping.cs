using Inflatable.BaseClasses;
using Mithril.Communication.Abstractions.Events;
using Mithril.Data.Abstractions.Databases;

namespace Mithril.Communication.Abstractions.Mappings
{
    /// <summary>
    /// Message sent event mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass&lt;MessageSentEvent, DefaultDatabase&gt;"/>
    public class MessageSentEventMapping : MappingBaseClass<MessageSentEvent, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageSentEventMapping"/> class.
        /// </summary>
        public MessageSentEventMapping()
        {
            Map(x => x.Message);
            Reference(x => x.Status);
        }
    }
}