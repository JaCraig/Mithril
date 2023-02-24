using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;

namespace Mithril.Communication.Email.Models.Mappings
{
    /// <summary>
    /// Email settings mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass&lt;EmailSettings, DefaultDatabase&gt;"/>
    public class EmailSettingsMapping : MappingBaseClass<EmailSettings, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSettingsMapping"/> class.
        /// </summary>
        public EmailSettingsMapping()
        {
            Reference(x => x.LocalDomain);
            Reference(x => x.Password);
            Reference(x => x.Port);
            Reference(x => x.Server);
            Reference(x => x.SystemAddress);
            Reference(x => x.UserName);
            Reference(x => x.UseSSL);
        }
    }
}