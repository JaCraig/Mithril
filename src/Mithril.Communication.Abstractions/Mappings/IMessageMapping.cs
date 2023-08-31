using Inflatable.BaseClasses;
using Mithril.Communication.Abstractions.Interfaces;
using Mithril.Data.Abstractions.Databases;

namespace Mithril.Communication.Abstractions.Mappings
{
    /// <summary>
    /// Message interface mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass&lt;IMessage, DefaultDatabase&gt;"/>
    public class IMessageMapping : MappingBaseClass<IMessage, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IMessageMapping"/> class.
        /// </summary>
        public IMessageMapping()
        {
            _ = Reference(x => x.Application);
            _ = ManyToOne<Attachment>(x => x.Attachments!);
            _ = Reference(x => x.BCC);
            _ = Reference(x => x.Body);
            _ = Reference(x => x.CC);
            _ = Reference(x => x.From);
            _ = Reference(x => x.Subject);
            _ = Reference(x => x.To);
            _ = Reference(x => x.Template);
            _ = Reference(x => x.TemplateData);
        }
    }
}