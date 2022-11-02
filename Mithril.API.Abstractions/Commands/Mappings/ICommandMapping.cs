using Inflatable.BaseClasses;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.Data.Inflatable.Databases;

namespace Mithril.API.Abstractions.Commands.Mappings
{
    /// <summary>
    /// Command mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass{ICommand, DefaultDatabase}"/>
    public class ICommandMapping : MappingBaseClass<ICommand, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ICommandMapping"/> class.
        /// </summary>
        public ICommandMapping()
        {
        }
    }
}