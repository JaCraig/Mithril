using Mithril.Data.Abstractions.Enums;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Mithril.Data.Abstractions.Interfaces
{
    /// <summary>
    /// User interface
    /// </summary>
    /// <seealso cref="IModel"/>
    public interface IUser : IModel, IEquatable<IUser>
    {
        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>The full name.</value>
        string FullName { get; }

        /// <summary>
        /// Gets the initials.
        /// </summary>
        /// <value>The initials.</value>
        string Initials { get; }

        /// <summary>
        /// Gets the short name.
        /// </summary>
        /// <value>The short name.</value>
        string ShortName { get; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        [MaxLength(100)]
        string? UserName { get; }

        /// <summary>
        /// Determines whether this instance can access the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name.</param>
        /// <returns><c>true</c> if this instance can access the specified type; otherwise, <c>false</c>.</returns>
        bool CanAccess(string type, string? name);

        /// <summary>
        /// Determines if this user object is equal to the user specified.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>True if they are the same, false otherwise.</returns>
        bool Equals(ClaimsPrincipal? user);

        /// <summary>
        /// Gets the claims requested.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The claims specified.</returns>
        IEnumerable<IUserClaim> GetClaims(UserClaimTypes type);
    }
}