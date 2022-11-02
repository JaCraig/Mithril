using Inflatable.BaseClasses;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.Data.Inflatable.Databases;

namespace Mithril.API.Abstractions.Commands.Mappings
{
    /// <summary>
    /// Event mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass{IEvent, DefaultDatabase}"/>
    public class ICommandEventMapping : MappingBaseClass<ICommandEvent, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ICommandEventMapping"/> class.
        /// </summary>
        public ICommandEventMapping()
        {
        }
    }
}