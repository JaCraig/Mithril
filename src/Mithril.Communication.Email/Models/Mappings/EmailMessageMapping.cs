using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;

namespace Mithril.Communication.Email.Models.Mappings
{
    /// <summary>
    /// Email message mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass&lt;EmailMessage, DefaultDatabase&gt;"/>
    public class EmailMessageMapping : MappingBaseClass<EmailMessage, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailMessageMapping"/> class.
        /// </summary>
        public EmailMessageMapping()
        {
        }
    }
}