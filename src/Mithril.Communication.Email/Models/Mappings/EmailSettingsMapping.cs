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
            _ = Reference(x => x.LocalDomain);
            _ = Reference(x => x.Password);
            _ = Reference(x => x.Port);
            _ = Reference(x => x.Server);
            _ = Reference(x => x.SystemAddress);
            _ = Reference(x => x.UserName);
            _ = Reference(x => x.UseSSL);
        }
    }
}