using Mithril.Content.Abstractions.Interfaces;
using System.Security.Claims;

namespace Mithril.Admin.Abstractions.Interfaces
{
    /// <summary>
    /// Editor interface, used to define an admin page component
    /// </summary>
    public interface IEditor
    {
        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <value>The category.</value>
        string Category { get; }

        /// <summary>
        /// Gets the component definition.
        /// </summary>
        /// <value>The component definition.</value>
        IComponentDefinition ComponentDefinition { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        string Description { get; }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <value>The icon.</value>
        string Icon { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Determines whether this instance can be viewed by the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// <c>true</c> if this instance can be viewed by the specified user; otherwise, <c>false</c>.
        /// </returns>
        bool CanView(ClaimsPrincipal? user);
    }
}