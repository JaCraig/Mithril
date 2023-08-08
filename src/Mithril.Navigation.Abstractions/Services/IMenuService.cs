using Mithril.Navigation.Abstractions.Interfaces;
using System.Security.Claims;

namespace Mithril.Navigation.Abstractions.Services
{
    /// <summary>
    /// Menu service interface
    /// </summary>
    public interface IMenuService
    {
        /// <summary>
        /// Creates a menu builder.
        /// </summary>
        /// <param name="display">The display name of the menu.</param>
        /// <param name="user">The user.</param>
        /// <returns>
        /// The menu builder.
        /// </returns>
        IMenuBuilder? CreateMenuBuilder(string display, ClaimsPrincipal? user);
    }
}