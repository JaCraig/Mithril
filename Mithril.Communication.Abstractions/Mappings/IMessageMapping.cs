using Inflatable.BaseClasses;
using Mithril.Communication.Abstractions.Interfaces;
using Mithril.Data.Inflatable.Databases;

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
            Reference(x => x.Application);
            ManyToOne<Attachment>(x => x.Attachments!);
            Reference(x => x.BCC);
            Reference(x => x.Body);
            Reference(x => x.CC);
            Reference(x => x.From);
            Reference(x => x.Subject);
            Reference(x => x.To);
            Reference(x => x.Template);
            Reference(x => x.TemplateData);
        }
    }
}