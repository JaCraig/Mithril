using Inflatable.BaseClasses;
using Mithril.Communication.Abstractions.Commands;
using Mithril.Data.Inflatable.Databases;

namespace Mithril.Communication.Abstractions.Mappings
{
    /// <summary>
    /// Send message command mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass&lt;SendMessageCommand, DefaultDatabase&gt;"/>
    public class SendMessageCommandMapping : MappingBaseClass<SendMessageCommand, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendMessageCommandMapping"/> class.
        /// </summary>
        public SendMessageCommandMapping()
        {
            Map(x => x.Message).CascadeChanges();
        }
    }
}