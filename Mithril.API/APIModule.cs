using Canister.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Mithril.API.Query.Interfaces;
using Mithril.Core.Abstractions.Modules.BaseClasses;

namespace Mithril.API
{
    /// <summary>
    /// API Module
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;APIModule&gt;"/>
    public class APIModule : ModuleBaseClass<APIModule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="APIModule"/> class.
        /// </summary>
        public APIModule()
            : base("API Module", "API", "API")
        {
        }

        /// <summary>
        /// Loads the module using the bootstrapper
        /// </summary>
        /// <param name="bootstrapper">The bootstrapper.</param>
        public override void Load(IBootstrapper? bootstrapper)
        {
            bootstrapper?.RegisterAll<IQuery>(ServiceLifetime.Singleton);
        }
    }
}