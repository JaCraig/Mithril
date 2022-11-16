using Inflatable.BaseClasses;
using Mithril.Data.Inflatable.Databases;

namespace Mithril.Logging.Serilog.Models.Mappings
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
            Reference(x => x.LogLevel);
            Reference(x => x.Message);
        }
    }
}