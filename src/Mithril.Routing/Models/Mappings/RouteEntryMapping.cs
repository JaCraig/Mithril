using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;

namespace Mithril.Routing.Models.Mappings
{
    /// <summary>
    /// Route entry mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass{RouteEntry, DefaultDatabase}"/>
    public class RouteEntryMapping : MappingBaseClass<RouteEntry, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RouteEntryMapping"/> class.
        /// </summary>
        public RouteEntryMapping()
        {
        }
    }
}