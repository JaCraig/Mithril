using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;

namespace Mithril.Logging.Models.Mappings
{
    /// <summary>
    /// Log command mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass&lt;LogCommand, DefaultDatabase&gt;"/>
    public class LogCommandMapping : MappingBaseClass<LogCommand, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogCommandMapping"/> class.
        /// </summary>
        public LogCommandMapping()
        {
            _ = Reference(x => x.LogLevel);
            _ = Reference(x => x.Message);
        }
    }
}