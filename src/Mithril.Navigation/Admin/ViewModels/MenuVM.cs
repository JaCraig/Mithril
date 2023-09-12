using BigBook;
using Microsoft.Extensions.DependencyInjection;
using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Data.Abstractions.Services;
using Mithril.Navigation.Models;
using Mithril.Security.Abstractions.Admin.ViewModels;
using Mithril.Security.Abstractions.Enums;
using Mithril.Security.Abstractions.Services;
using System.Security.Claims;

namespace Mithril.Navigation.Admin.ViewModels
{
    /// <summary>
    /// Menu VM
    /// </summary>
    /// <seealso cref="EntityBaseClass&lt;Menu&gt;" />
    public class MenuVM : EntityBaseClass<Menu>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuVM"/> class.
        /// </summary>
        public MenuVM()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuVM" /> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="full">if set to <c>true</c> [full].</param>
        public MenuVM(Menu? entity, bool full) : base(entity)
        {
            if (entity is null)
                return;
            Display = entity.Display;
            if (!full)
                return;
            WhoCanView = entity.Permissions?.Claims.ToList(claim => new ClaimDropDownVM(claim)) ?? new List<ClaimDropDownVM>();
            Links = entity.Items.ToList(menuItem => new MenuItemVM(menuItem));
        }

        /// <summary>
        /// Gets or sets the display.
        /// </summary>
        /// <value>
        /// The display.
        /// </value>
        [Order(1)]
        public string? Display { get; set; }

        /// <summary>
        /// Gets or sets the links.
        /// </summary>
        /// <value>
        /// The links.
        /// </value>
        [Order(2)]
        public List<MenuItemVM> Links { get; set; } = new List<MenuItemVM>();

        /// <summary>
        /// Gets or sets the who can view.
        /// </summary>
        /// <value>
        /// The who can view.
        /// </value>
        [Order(3)]
        public List<ClaimDropDownVM> WhoCanView { get; set; } = new List<ClaimDropDownVM>();

        /// <summary>
        /// Saves the changes asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dataService">The data service.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="currentUser">The current user.</param>
        /// <returns>
        /// The async task.
        /// </returns>
        public override async Task<Menu?> SaveAsync(long id, IDataService? dataService, IServiceProvider? serviceProvider, ClaimsPrincipal? currentUser)
        {
            if (string.IsNullOrEmpty(Display) || serviceProvider is null || dataService is null)
                return null;
            var SecurityService = serviceProvider.GetService<ISecurityService>();
            var MenuObject = Menu.Load(id, dataService) ?? new Menu(Display);
            MenuObject.Display = Display;
            await SetupLinksAsync(MenuObject, dataService, SecurityService, currentUser).ConfigureAwait(false);
            await SetupClaimsAsync(MenuObject, dataService, SecurityService, currentUser).ConfigureAwait(false);
            await MenuObject.SaveAsync(dataService, currentUser).ConfigureAwait(false);
            return MenuObject;
        }

        /// <summary>
        /// Adds the items asynchronous.
        /// </summary>
        /// <param name="menuObject">The menu object.</param>
        /// <param name="dataService">The data service.</param>
        /// <param name="securityService">The security service.</param>
        /// <param name="currentUser">The current user.</param>
        private async Task AddItemsAsync(Menu menuObject, IDataService dataService, ISecurityService securityService, ClaimsPrincipal? currentUser)
        {
            foreach (var Link in Links)
            {
                if (Link is null)
                    continue;
                var MenuItem = menuObject.AddOrUpdateMenuItem(Link.Display, Link.Description, Link.Icon, Link.Url, Link.Order);
                if (Link.WhoCanView.Count == 0)
                {
                    if (MenuItem.Permissions is null)
                        continue;
                    await MenuItem.Permissions.DeleteAsync(dataService, currentUser, false).ConfigureAwait(false);
                    continue;
                }
                MenuItem.Permissions ??= (await securityService.LoadOrCreatePermissionAsync(Guid.NewGuid().ToString(), PermissionType.Any).ConfigureAwait(false));
                foreach (var Claim in Link.WhoCanView.Select(x => securityService.LoadClaim(x.Claim)))
                {
                    MenuItem.Permissions.AddClaim(Claim);
                }
                await MenuItem.Permissions.SaveAsync(dataService, currentUser).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Removes the items asynchronously.
        /// </summary>
        /// <param name="menuObject">The menu object.</param>
        /// <param name="dataService">The data service.</param>
        /// <param name="currentUser">The current user.</param>
        /// <returns>The async task.</returns>
        private Task RemoveItemsAsync(Menu menuObject, IDataService dataService, ClaimsPrincipal? currentUser)
        {
            var ItemsToRemove = menuObject.Items.Where(x => !Links.Any(y => y.Display == x.Display)).ToList();
            for (var X = 0; X < ItemsToRemove.Count; ++X)
            {
                _ = menuObject.Items.Remove(ItemsToRemove[X]);
            }
            return dataService.DeleteAsync(currentUser, ItemsToRemove.ToArray());
        }

        /// <summary>
        /// Setups the claims asynchronous.
        /// </summary>
        /// <param name="menuObject">The menu object.</param>
        /// <param name="dataService">The data service.</param>
        /// <param name="securityService">The security service.</param>
        /// <param name="currentUser">The current user.</param>
        private async Task SetupClaimsAsync(Menu menuObject, IDataService? dataService, ISecurityService? securityService, ClaimsPrincipal? currentUser)
        {
            if (WhoCanView.Count == 0 || securityService is null)
                return;
            menuObject.Permissions ??= (await securityService.LoadOrCreatePermissionAsync(Guid.NewGuid().ToString(), PermissionType.Any).ConfigureAwait(false));
            menuObject.Permissions.Claims.Clear();
            foreach (var Claim in WhoCanView.Select(x => securityService.LoadClaim(x.Claim)))
            {
                menuObject.Permissions.AddClaim(Claim);
            }
            await menuObject.Permissions.SaveAsync(dataService, currentUser).ConfigureAwait(false);
        }

        /// <summary>
        /// Setups the links asynchronous.
        /// </summary>
        /// <param name="menuObject">The menu object.</param>
        /// <param name="dataService">The data service.</param>
        /// <param name="securityService">The security service.</param>
        /// <param name="currentUser">The current user.</param>
        private async Task SetupLinksAsync(Menu menuObject, IDataService dataService, ISecurityService? securityService, ClaimsPrincipal? currentUser)
        {
            if ((Links?.Count ?? 0) == 0 || securityService is null)
                return;
            await RemoveItemsAsync(menuObject, dataService, currentUser).ConfigureAwait(false);
            await AddItemsAsync(menuObject, dataService, securityService, currentUser).ConfigureAwait(false);
        }
    }
}