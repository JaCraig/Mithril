using Mithril.Core.Abstractions.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Mithril.Core.Abstractions.Security.Interfaces
{
    /// <summary>
    /// User claim interface
    /// </summary>
    /// <seealso cref="IModel"/>
    public interface IUserClaim : IModel, IEquatable<IUserClaim>
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [Required]
        [MinLength(1)]
        [MaxLength(128)]
        string? Type { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>The users.</value>
        IList<IUser> Users { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [Required]
        [MinLength(1)]
        string? Value { get; set; }

        /// <summary>
        /// Determines whether this instance can access the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if this instance can access the specified type; otherwise, <c>false</c>.</returns>
        bool CanAccess(string type, string? value);

        /// <summary>
        /// Determines whether this instance can access the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if this instance can access the specified user; otherwise, <c>false</c>.</returns>
        bool CanAccess(IUser user);

        /// <summary>
        /// Determines whether this instance can access the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if this instance can access the specified user; otherwise, <c>false</c>.</returns>
        bool CanAccess(ClaimsPrincipal user);
    }
}