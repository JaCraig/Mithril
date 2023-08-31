using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;

namespace Mithril.Communication.Abstractions.Mappings
{
    /// <summary>
    /// Attachment mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass&lt;EmailAttachment, DefaultDatabase&gt;"/>
    public class AttachmentMapping : MappingBaseClass<Attachment, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentMapping"/> class.
        /// </summary>
        public AttachmentMapping()
        {
            _ = Reference(x => x.FileHash);
            _ = Reference(x => x.FileName);
            _ = Reference(x => x.Location);
            _ = Reference(x => x.Size);
        }
    }
}