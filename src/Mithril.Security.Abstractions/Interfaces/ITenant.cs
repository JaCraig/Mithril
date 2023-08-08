using Mithril.Data.Abstractions.Interfaces;

namespace Mithril.Security.Abstractions.Interfaces
{
    /// <summary>
    /// Tenant interface
    /// </summary>
    /// <seealso cref="IModel"/>
    public interface ITenant : IModel, IEquatable<ITenant>
    {
        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>The full name.</value>
        string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>The users.</value>
        IList<IUser> Users { get; set; }
    }
}