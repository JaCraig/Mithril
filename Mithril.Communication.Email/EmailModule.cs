using Mithril.Communication.Email.Models;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using Mithril.Data.Abstractions.Services;

namespace Mithril.Communication.Email
{
    /// <summary>
    /// Email module
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;EmailModule&gt;"/>
    public class EmailModule : ModuleBaseClass<EmailModule>
    {
        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <param name="dataService">Data service</param>
        /// <param name="services">The services for the application.</param>
        /// <returns>The async task.</returns>
        public override Task InitializeDataAsync(IDataService? dataService, IServiceProvider? services)
        {
            return EmailSettings.LoadOrCreateAsync(dataService);
        }
    }
}