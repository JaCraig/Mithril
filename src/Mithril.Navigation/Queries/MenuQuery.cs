using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Mithril.API.Abstractions.Query;
using Mithril.API.Abstractions.Query.BaseClasses;
using Mithril.API.Abstractions.Query.Interfaces;
using Mithril.Data.Abstractions.ExtensionMethods;
using Mithril.Data.Abstractions.Services;
using Mithril.Navigation.Models;
using System.Security.Claims;

namespace Mithril.Navigation.Queries
{
    /// <summary>
    /// Menu query
    /// </summary>
    /// <seealso cref="QueryBaseClass&lt;MenuVM&gt;" />
    public class MenuQuery : QueryBaseClass<MenuVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuQuery"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="featureManager">The feature manager.</param>
        /// <param name="dataService">The data service.</param>
        public MenuQuery(ILogger<MenuQuery>? logger, IFeatureManager? featureManager, IDataService? dataService)
            : base(logger, featureManager)
        {
            DataService = dataService;
        }

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        /// <value>
        /// The arguments.
        /// </value>
        public override IArgument[] Arguments { get; } = new IArgument[]
        {
            new Argument<string>(){ Description="Name of the menu", Name="name"}
        };

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public override string Name => "Menu";

        /// <summary>
        /// Gets the data service.
        /// </summary>
        /// <value>
        /// The data service.
        /// </value>
        private IDataService? DataService { get; }

        /// <summary>
        /// Used to resolve the data asked for by the query.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>
        /// The data specified.
        /// </returns>
        public override Task<MenuVM?> ResolveAsync(ClaimsPrincipal? user, Arguments arguments)
        {
            if (user is null || arguments is null)
                return Task.FromResult<MenuVM?>(null);
            var MenuName = arguments.GetValue<string>("name") ?? "Default";
            return !user.TryGetTennant(out var Tennant)
                ? Task.FromResult<MenuVM?>(null)
                : Task.FromResult<MenuVM?>(new MenuVM(LoadMenu(MenuName, Tennant), user));
        }

        /// <summary>
        /// Loads the menu.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="tennantID">The tennant identifier.</param>
        /// <returns>The Menu specified.</returns>
        private Menu? LoadMenu(string displayName, long tennantID) => Menu.Query(DataService)?.Where(x => x.TenantID == tennantID && x.Display == displayName).FirstOrDefault();
    }
}