using BigBook;
using Mithril.Data.Abstractions.BaseClasses;
using Mithril.Data.Abstractions.Interfaces;
using Mithril.Data.Abstractions.Services;
using Mithril.Security.Abstractions.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Mithril.Security.Models
{
    /// <summary>
    /// Tenant model class
    /// </summary>
    /// <seealso cref="ITenant"/>
    /// <seealso cref="ModelBase&lt;Tenant&gt;"/>
    /// <seealso cref="IEquatable&lt;Tenant&gt;"/>
    public class Tenant : ModelBase<Tenant>, ITenant, IEquatable<Tenant>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Tenant"/> class.
        /// </summary>
        public Tenant()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tenant"/> class.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <exception cref="ArgumentException">displayName</exception>
        public Tenant(string displayName)
        {
            if (!string.IsNullOrEmpty(displayName) && displayName.Length > 100)
                throw new ArgumentException(nameof(displayName) + " has a max length of 100 characters.");
            DisplayName = displayName;
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>The users.</value>
        public virtual IList<IUser> Users { get; set; } = new List<IUser>();

        /// <summary>
        /// Loads the specified tenant by display name.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="dataService">The data service.</param>
        /// <returns>The tenant specified.</returns>
        public static Tenant? Load(string displayName, IDataService? dataService) => Query(dataService)?.Where(x => x.DisplayName == displayName).FirstOrDefault();

        /// <summary>
        /// Loads or creates the Tenant if necessary.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="context">The context.</param>
        /// <param name="user">The user.</param>
        /// <returns>The Tenant specified.</returns>
        public static async Task<Tenant> LoadOrCreateAsync(string displayName, IDataService? context, ClaimsPrincipal? user)
        {
            Tenant? ReturnValue = Load(displayName, context);
            if (ReturnValue is null)
            {
                ReturnValue = new Tenant(displayName);
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
        public static bool operator !=(Tenant? left, Tenant? right) => !(left == right);

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(Tenant? left, Tenant? right) => left is null ? right is null : left.CompareTo(right) < 0;

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(Tenant? left, Tenant? right) => left is null ? right is null : left.CompareTo(right) <= 0;

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Tenant? first, Tenant? second)
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
        public static bool operator >(Tenant? left, Tenant? right) => left is null ? right is null : left.CompareTo(right) > 0;

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(Tenant? left, Tenant? right) => left is null ? right is null : left.CompareTo(right) >= 0;

        /// <summary>
        /// Compares the object to another object
        /// </summary>
        /// <param name="other">Object to compare to</param>
        /// <returns>0 if they are equal, -1 if this is smaller, 1 if it is larger</returns>
        public override int CompareTo(Tenant? other) => other is null ? 1 : ID.CompareTo(other.ID);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(ITenant? other) => CompareTo(other) == 0;

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(Tenant? other) => CompareTo(other) == 0;

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
        /// Loads the user or creates them asynchronously.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="dataService">The data service.</param>
        /// <param name="user">The user.</param>
        /// <param name="claims">The claims.</param>
        /// <returns>The user specified.</returns>
        public async Task<User> LoadOrCreateUserAsync(string userName, string firstName, string lastName, IDataService? dataService, ClaimsPrincipal? user, params IUserClaim[] claims)
        {
            claims ??= Array.Empty<IUserClaim>();
            var ReturnValue = User.Load(userName, dataService);
            if (ReturnValue is null)
            {
                ReturnValue = new User(userName, firstName, lastName, this);
                for (int i = 0, claimsLength = claims.Length; i < claimsLength; i++)
                {
                    IUserClaim? Role = claims[i];
                    _ = ReturnValue.AddClaim(Role);
                }
                Users ??= new List<IUser>();
                Users.Add(ReturnValue);
                if (dataService is not null)
                    _ = await dataService.SaveAsync(user, this).ConfigureAwait(false);
            }
            return ReturnValue;
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this instance.</returns>
        public override string ToString() => string.IsNullOrEmpty(DisplayName) ? "New Tenant" : DisplayName;
    }
}