using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;

namespace Mithril.Communication.Abstractions
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
            Reference(x => x.FileHash);
            Reference(x => x.FileName);
            Reference(x => x.Location);
            Reference(x => x.Size);
        }
    }
}