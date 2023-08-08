using Inflatable.BaseClasses;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.Data.Abstractions.Databases;

namespace Mithril.API.Abstractions.Commands.Mappings
{
    /// <summary>
    /// Event mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass{IEvent, DefaultDatabase}"/>
    public class IEventMapping : MappingBaseClass<IEvent, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IEventMapping"/> class.
        /// </summary>
        public IEventMapping()
        {
            Reference(x => x.RetryCount);
            Reference(x => x.State).WithMaxLength(20);
        }
    }
}