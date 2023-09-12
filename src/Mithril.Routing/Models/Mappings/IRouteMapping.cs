using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;
using Mithril.Routing.Abstractions.Interfaces;

namespace Mithril.Routing.Models.Mappings
{
    /// <summary>
    /// IRoute mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass&lt;IRoute, DefaultDatabase&gt;" />
    public class IRouteMapping : MappingBaseClass<IRoute, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IRouteMapping"/> class.
        /// </summary>
        public IRouteMapping()
            : base(merge: true)
        {
            _ = Reference(x => x.InputPath).IsUnique();
            _ = Reference(x => x.OutputPath);
        }
    }
}