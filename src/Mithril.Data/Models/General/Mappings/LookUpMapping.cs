using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;

namespace Mithril.Data.Models.General.Mappings
{
    /// <summary>
    /// LookUp mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass&lt;LookUp, DefaultDatabase&gt;"/>
    public class LookUpMapping : MappingBaseClass<LookUp, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LookUpMapping"/> class.
        /// </summary>
        public LookUpMapping()
        {
        }
    }
}