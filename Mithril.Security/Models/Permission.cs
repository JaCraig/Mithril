using Mithril.Data.Abstractions.BaseClasses;
using Mithril.Data.Abstractions.Interfaces;
using Mithril.Data.Abstractions.Services;
using Mithril.Security.Abstractions.Enums;
using Mithril.Security.Abstractions.Interfaces;
using System.Security.Claims;

namespace Mithril.Security.Models
{
    /// <summary>
    /// Permission data holder
    /// </summary>
    /// <seealso cref="IPermission"/>
    /// <seealso cref="IEquatable{Permission}"/>
    /// <seealso cref="ModelBase{Permission}"/>
    public class Permission : ModelBase<Permission>, IPermission, IEquatable<Permission>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Permission"/> class.
        /// </summary>
        public Permission()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Permission"/> class.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="operand">The operand.</param>
        /// <param name="claims">The claims.</param>
        /// <exception cref="ArgumentNullException">claims</exception>
        /// <exception cref="ArgumentException">displayName</exception>
        public Permission(string displayName, PermissionType operand, params IUserClaim[] claims)
        {
            if (claims is null)
                throw new ArgumentNullException(nameof(claims));
            if (string.IsNullOrEmpty(displayName))
                throw new ArgumentNullException(nameof(displayName));
            if (displayName.Length > 100)
                throw new ArgumentException(nameof(displayName) + " is too long. Max of 100 characters allowed.");
            DisplayName = displayName;
            Operand = operand;
            Claims = claims.Where(x => x != null).ToList();
        }

        /// <summary>
        /// Gets or sets the claims.
        /// </summary>
        /// <value>The claims.</value>
        public virtual IList<IUserClaim> Claims { get; set; } = new List<IUserClaim>();

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the operand.
        /// </summary>
        /// <value>The operand.</value>
        public PermissionType Operand { get; set; }

        /// <summary>
        /// Loads a specific permission
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="dataService">The data service.</param>
        /// <returns>Permission specified</returns>
        public static Permission? Load(string displayName, IDataService dataService)
        {
            return Query(dataService).Where(x => x.DisplayName == displayName).FirstOrDefault();
        }

        /// <summary>
        /// Loads a specific claim or creates it.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="operand">The operand.</param>
        /// <param name="claims">The claims.</param>
        /// <param name="context">The context.</param>
        /// <returns>The user claim specified.</returns>
        public static async Task<IPermission> LoadOrCreateAsync(string displayName, PermissionType operand, IUserClaim[] claims, IDataService context)
        {
            var ReturnValue = Load(displayName, context);
            if (ReturnValue is null)
            {
                claims ??= Array.Empty<IUserClaim>();
                ReturnValue = new Permission(displayName, operand, claims);
                await context.SaveAsync(ReturnValue).ConfigureAwait(false);
            }
            return ReturnValue;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Permission? left, Permission? right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(Permission? left, Permission? right)
        {
            return left is null ? right is null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(Permission? left, Permission? right)
        {
            return left is null ? right is null : left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Permission? first, Permission? second)
        {
            return ReferenceEquals(first, second)
                || (first is not null
                    && second is not null
                    && first.CompareTo(second) == 0);
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(Permission? left, Permission? right)
        {
            return left is null ? right is null : left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(Permission? left, Permission? right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Adds the claim.
        /// </summary>
        /// <param name="claim">The claim.</param>
        /// <returns>This.</returns>
        public IPermission AddClaim(IUserClaim claim)
        {
            if (Claims.Contains(claim))
                return this;
            Claims.Add(claim);
            return this;
        }

        /// <summary>
        /// Compares the object to another object
        /// </summary>
        /// <param name="other">Object to compare to</param>
        /// <returns>0 if they are equal, -1 if this is smaller, 1 if it is larger</returns>
        public override int CompareTo(Permission? other)
        {
            return base.CompareTo(other);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(Permission? other)
        {
            return base.Equals(other);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(IPermission? other)
        {
            return base.Equals(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/>, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data
        /// structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified user has permission.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if the specified user has permission; otherwise, <c>false</c>.</returns>
        public bool HasPermission(IUser? user)
        {
            return user?.Active == true
                && Claims.Count != 0
                && (Operand == PermissionType.Any
                    ? Claims.Any(x => x.CanAccess(user))
                    : Claims.All(x => x.CanAccess(user)));
        }

        /// <summary>
        /// Determines whether the specified user has permission.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if the specified user has permission; otherwise, <c>false</c>.</returns>
        public bool HasPermission(ClaimsPrincipal? user)
        {
            return user?.Identity?.IsAuthenticated == true
                && user.Claims.Any()
                && (Operand == PermissionType.Any
                    ? Claims.Any(x => x.CanAccess(user))
                    : Claims.All(x => x.CanAccess(user)));
        }

        /// <summary>
        /// Removes the claim.
        /// </summary>
        /// <param name="claim">The claim.</param>
        /// <returns>This.</returns>
        public IPermission RemoveClaim(IUserClaim claim)
        {
            if (!Claims.Contains(claim))
                return this;
            Claims.Remove(claim);
            return this;
        }

        /// <summary>
        /// Replaces the claim.
        /// </summary>
        /// <param name="originalClaim">The original claim.</param>
        /// <param name="newClaim">The new claim.</param>
        /// <returns>This</returns>
        public IPermission ReplaceClaim(IUserClaim originalClaim, IUserClaim newClaim)
        {
            if (Claims.Remove(originalClaim))
                Claims.Add(newClaim);
            return this;
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this instance.</returns>
        public override string ToString()
        {
            return DisplayName ?? "";
        }
    }
}