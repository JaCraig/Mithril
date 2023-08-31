using Mithril.Data.Abstractions.BaseClasses;
using Mithril.Data.Abstractions.Enums;
using Mithril.Data.Abstractions.Interfaces;
using Mithril.Data.Abstractions.Services;
using System.Security.Claims;

namespace Mithril.Security.Models
{
    /// <summary>
    /// User claim
    /// </summary>
    /// <seealso cref="IUserClaim"/>
    /// <seealso cref="IEquatable{UserClaim}"/>
    /// <seealso cref="ModelBase{UserClaim}"/>
    public class UserClaim : ModelBase<UserClaim>, IUserClaim, IEquatable<UserClaim>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaim"/> class.
        /// </summary>
        public UserClaim()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaim"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <param name="users">The users.</param>
        public UserClaim(UserClaimTypes type, string value, params IUser[] users)
            : this()
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));
            Value = value;
            users ??= Array.Empty<IUser>();
            Type = (string)type;
            Users = users.Where(x => x != null).ToList();
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>The users.</value>
        public virtual IList<IUser> Users { get; set; } = new List<IUser>();

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string? Value { get; set; }

        /// <summary>
        /// Loads a specific claim
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <param name="dataService">The data service.</param>
        /// <returns>User claim specified</returns>
        public static UserClaim? Load(UserClaimTypes type, string value, IDataService? dataService) => Query(dataService)?.Where(x => x.Type == type && x.Value == value).FirstOrDefault();

        /// <summary>
        /// Loads a specific claim or creates it.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <param name="context">The context.</param>
        /// <param name="user">The user.</param>
        /// <returns>The user claim specified.</returns>
        public static async Task<IUserClaim> LoadOrCreateAsync(UserClaimTypes type, string value, IDataService? context, ClaimsPrincipal? user)
        {
            UserClaim? ReturnValue = Load(type, value, context);
            if (ReturnValue is null)
            {
                ReturnValue = new UserClaim(type, value);
                if (context is not null)
                    _ = await context.SaveAsync(user, ReturnValue).ConfigureAwait(false);
            }
            return ReturnValue;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(UserClaim? left, UserClaim? right) => !(left == right);

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(UserClaim? left, UserClaim? right) => left is null ? right is null : left.CompareTo(right) < 0;

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(UserClaim? left, UserClaim? right) => left is null ? right is null : left.CompareTo(right) <= 0;

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(UserClaim? first, UserClaim? second)
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
        public static bool operator >(UserClaim? left, UserClaim? right) => left is null ? right is null : left.CompareTo(right) > 0;

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(UserClaim? left, UserClaim? right) => left is null ? right is null : left.CompareTo(right) >= 0;

        /// <summary>
        /// Determines whether this instance can access the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if this instance can access the specified type; otherwise, <c>false</c>.</returns>
        public bool CanAccess(string type, string? value)
        {
            return string.Equals(Type, type, StringComparison.OrdinalIgnoreCase)
                && string.Equals(Value, value, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Determines whether this instance can access the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if this instance can access the specified user; otherwise, <c>false</c>.</returns>
        public bool CanAccess(IUser user) => !string.IsNullOrEmpty(Type) && !string.IsNullOrEmpty(Value) && (user?.CanAccess(Type, Value) ?? false);

        /// <summary>
        /// Determines whether this instance can access the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if this instance can access the specified user; otherwise, <c>false</c>.</returns>
        public bool CanAccess(ClaimsPrincipal? user) => !string.IsNullOrEmpty(Type) && !string.IsNullOrEmpty(Value) && (user?.HasClaim(Type, Value) ?? false);

        /// <summary>
        /// Compares the object to another object
        /// </summary>
        /// <param name="other">Object to compare to</param>
        /// <returns>0 if they are equal, -1 if this is smaller, 1 if it is larger</returns>
        public override int CompareTo(UserClaim? other) => base.CompareTo(other);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(UserClaim? other) => base.Equals(other);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(IUserClaim? other) => base.Equals(other);

        /// <summary>
        /// Determines whether the specified <see cref="object"/>, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj) => base.Equals(obj);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data
        /// structures like a hash table.
        /// </returns>
        public override int GetHashCode() => base.GetHashCode();

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this instance.</returns>
        public override string ToString() => Type + " : " + Value;
    }
}