using Mithril.Core.Abstractions.Modules.BaseClasses;
using Mithril.Core.Abstractions.Modules.Features;
using Mithril.Core.Abstractions.Modules.Interfaces;

namespace Mithril.Logging.Javascript
{
    /// <summary>
    /// Module that adds a command handler that accepts specific logging added to Mithril
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;SerilogModule&gt;"/>
    public class LoggingModule : ModuleBaseClass<LoggingModule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SerilogModule"/> class.
        /// </summary>
        public LoggingModule()
        {
            Features = new IFeature[] { new LoggingFeature() };
        }
    }
}