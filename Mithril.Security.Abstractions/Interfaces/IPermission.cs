using Mithril.Data.Abstractions.Interfaces;
using Mithril.Security.Abstractions.Enums;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Mithril.Security.Abstractions.Interfaces
{
    /// <summary>
    /// Permission interface
    /// </summary>
    /// <seealso cref="IModel"/>
    public interface IPermission : IModel, IEquatable<IPermission>
    {
        /// <summary>
        /// Gets the claims.
        /// </summary>
        /// <value>The claims.</value>
        IList<IUserClaim> Claims { get; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        [Required]
        [MinLength(1)]
        [MaxLength(128)]
        string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the operand.
        /// </summary>
        /// <value>The operand.</value>
        PermissionType Operand { get; set; }

        /// <summary>
        /// Adds the claim.
        /// </summary>
        /// <param name="claim">The claim.</param>
        /// <returns>This.</returns>
        IPermission AddClaim(IUserClaim claim);

        /// <summary>
        /// Determines whether the specified user has permission.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if the specified user has permission; otherwise, <c>false</c>.</returns>
        bool HasPermission(IUser? user);

        /// <summary>
        /// Determines whether the specified user has permission.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if the specified user has permission; otherwise, <c>false</c>.</returns>
        bool HasPermission(ClaimsPrincipal? user);

        /// <summary>
        /// Removes the claim.
        /// </summary>
        /// <param name="claim">The claim.</param>
        /// <returns>This.</returns>
        IPermission RemoveClaim(IUserClaim claim);

        /// <summary>
        /// Replaces the claim.
        /// </summary>
        /// <param name="originalClaim">The original claim.</param>
        /// <param name="newClaim">The new claim.</param>
        /// <returns>This</returns>
        IPermission ReplaceClaim(IUserClaim originalClaim, IUserClaim newClaim);
    }
}