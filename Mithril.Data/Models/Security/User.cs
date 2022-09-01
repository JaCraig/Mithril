using BigBook;
using Mithril.Core.Abstractions.Data.BaseClasses;
using Mithril.Core.Abstractions.ExtensionMethods;
using Mithril.Core.Abstractions.Mvc.Context;
using Mithril.Core.Abstractions.Security.Enums;
using Mithril.Core.Abstractions.Security.Interfaces;
using Mithril.Core.Abstractions.Services;
using Mithril.Data.Enums;
using Mithril.Data.Models.Contact;
using Mithril.Data.Models.General;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Mithril.Data.Models.Security
{
    /// <summary>
    /// User model
    /// </summary>
    /// <seealso cref="ModelBase{User}"/>
    /// <seealso cref="IUser"/>
    public class User : ModelBase<User>, IUser, IEquatable<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <exception cref="ArgumentException">userName or firstName or lastName</exception>
        /// <exception cref="ArgumentNullException">firstName or lastName</exception>
        public User(string userName, string firstName, string lastName)
        {
            if (!string.IsNullOrEmpty(userName) && userName.Length > 100)
                throw new ArgumentException(nameof(userName) + " has a max length of 100 characters.");
            if (string.IsNullOrEmpty(firstName))
                throw new ArgumentNullException(nameof(firstName));
            if (firstName.Length > 100)
                throw new ArgumentException(nameof(firstName) + " has a max length of 100 characters.");
            if (string.IsNullOrEmpty(lastName))
                throw new ArgumentNullException(nameof(lastName));
            if (lastName.Length > 100)
                throw new ArgumentException(nameof(lastName) + " has a max length of 100 characters.");
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
        }

        /// <summary>
        /// Gets or sets the claims.
        /// </summary>
        /// <value>The claims.</value>
        public virtual IList<IUserClaim> Claims { get; set; } = new List<IUserClaim>();

        /// <summary>
        /// Gets or sets the contact information.
        /// </summary>
        /// <value>The contact information.</value>
        public virtual IList<ContactInfo> ContactInformation { get; set; } = new List<ContactInfo>();

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>The full name.</value>
        public string FullName
        {
            get
            {
                return (string.IsNullOrEmpty(LastName) ? "" : LastName + ", ")
                    + (string.IsNullOrEmpty(Prefix) ? "" : Prefix + " ")
                    + FirstName
                    + (string.IsNullOrEmpty(MiddleName) ? "" : " " + MiddleName)
                    + (string.IsNullOrEmpty(NickName) ? "" : " \"" + NickName + "\"")
                    + (string.IsNullOrEmpty(Suffix) ? "" : " " + Suffix);
            }
        }

        /// <summary>
        /// Gets the initials.
        /// </summary>
        /// <value>The initials.</value>
        public string Initials
        {
            get
            {
                return string.IsNullOrEmpty(MiddleName)
                    ? new string(new char[] { FirstName?[0] ?? ' ', LastName?[0] ?? ' ' })
                    : new string(new char[] { FirstName?[0] ?? ' ', MiddleName[0], LastName?[0] ?? ' ' });
            }
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the name of the middle.
        /// </summary>
        /// <value>The name of the middle.</value>
        [MaxLength(100)]
        public string? MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the nick name.
        /// </summary>
        /// <value>The nick name.</value>
        [MaxLength(100)]
        public string? NickName { get; set; }

        /// <summary>
        /// Gets or sets the prefix.
        /// </summary>
        /// <value>The prefix.</value>
        [MaxLength(30)]
        public string? Prefix { get; set; }

        /// <summary>
        /// Gets the short name.
        /// </summary>
        /// <value>The short name.</value>
        public string ShortName
        {
            get
            {
                if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
                    return "";
                return FirstName +
                    (string.IsNullOrEmpty(MiddleName) ? " " : " " + MiddleName + " ") +
                    LastName;
            }
        }

        /// <summary>
        /// Gets or sets the suffix.
        /// </summary>
        /// <value>The suffix.</value>
        [MaxLength(30)]
        public string? Suffix { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [MaxLength(100)]
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string? UserName { get; set; }

        /// <summary>
        /// Loads the specified user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="dataService">The data service.</param>
        /// <returns>The user specified.</returns>
        public static User? Load(string userName, IDataService dataService)
        {
            return Query(dataService).Where(x => x.UserName == userName).FirstOrDefault();
        }

        /// <summary>
        /// Loads the current user.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <returns>The current user.</returns>
        public static User? LoadCurrentUser(IDataService dataService)
        {
            var UserName = HttpContext.Current?.User?.GetName();
            return string.IsNullOrEmpty(UserName) ? null : Load(UserName, dataService);
        }

        /// <summary>
        /// Loads or creates the user if necessary.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="context">The context.</param>
        /// <param name="claims">The claims.</param>
        /// <returns>The user specified.</returns>
        public static async Task<User> LoadOrCreateAsync(string userName, string firstName, string lastName, IDataService context, params IUserClaim[] claims)
        {
            claims ??= Array.Empty<IUserClaim>();
            var ReturnValue = Load(userName, context);
            if (ReturnValue is null)
            {
                ReturnValue = new User(userName, firstName, lastName);
                for (int i = 0, claimsLength = claims.Length; i < claimsLength; i++)
                {
                    var Role = claims[i];
                    ReturnValue.AddClaim(Role);
                }
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
        public static bool operator !=(User? left, User? right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(User? left, User? right)
        {
            return left is null ? right is null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(User? left, User? right)
        {
            return left is null ? right is null : left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(User? first, User? second)
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
        public static bool operator >(User? left, User? right)
        {
            return left is null ? right is null : left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(User? left, User? right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Adds the claim.
        /// </summary>
        /// <param name="claim">The claim.</param>
        /// <returns>This.</returns>
        public User AddClaim(IUserClaim claim)
        {
            if (Claims.Contains(claim))
                return this;
            Claims.Add(claim);
            return this;
        }

        /// <summary>
        /// Determines whether this instance can access the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The value.</param>
        /// <returns><c>true</c> if this instance can access the specified type; otherwise, <c>false</c>.</returns>
        public bool CanAccess(string type, string? name)
        {
            return Claims.Any(x => x.CanAccess(type, name));
        }

        /// <summary>
        /// Compares the object to another object
        /// </summary>
        /// <param name="other">Object to compare to</param>
        /// <returns>0 if they are equal, -1 if this is smaller, 1 if it is larger</returns>
        public override int CompareTo(User? other)
        {
            return other is null ? 1 : ID.CompareTo(other.ID);
        }

        /// <summary>
        /// Creates the contact info object or updates it asynchronously.
        /// </summary>
        /// <param name="type">The display name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The async task</returns>
        public async Task CreateOrUpdateContactInfoAsync(IDataService dataService, ContactInfoType type, params string[] value)
        {
            if (dataService is null)
                return;
            var ContactType = await LookUp.LoadOrCreateAsync(type, LookUpTypeEnum.ContactInfoType, type.Icon ?? "", dataService).ConfigureAwait(false);
            value ??= Array.Empty<string>();
            var Contacts = GetContactInfo(type).ToArray();
            int x = 0;
            if (Contacts.Length <= value.Length)
            {
                for (; x < Contacts.Length; ++x)
                {
                    Contacts[x].Info = value[x];
                }
                for (; x < value.Length; ++x)
                {
                    ContactInformation.Add(new ContactInfo(value[x], ContactType));
                }
                return;
            }
            for (; x < value.Length; ++x)
            {
                Contacts[x].Info = value[x];
            }
            for (; x < Contacts.Length; ++x)
            {
                ContactInformation.Remove(Contacts[x]);
                await Contacts[x].DeleteAsync(dataService, false).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(IUser? other)
        {
            return CompareTo(other) == 0;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(User? other)
        {
            return CompareTo(other) == 0;
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
        /// Determines if this user object is equal to the user specified.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>True if they are the same, false otherwise.</returns>
        public bool Equals(ClaimsPrincipal? user)
        {
            return user is not null && string.Equals(UserName, user.GetName(), StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Gets the claims of the type specified.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The list of user claims specified.</returns>
        public IEnumerable<IUserClaim> GetClaims(UserClaimTypes type)
        {
            return Claims.Where(x => x.Type == type);
        }

        /// <summary>
        /// Gets the contact information requested.
        /// </summary>
        /// <param name="types">The display name.</param>
        /// <returns>The contact info specified.</returns>
        public IEnumerable<ContactInfo> GetContactInfo(params ContactInfoType[] types)
        {
            return ContactInformation.Where(x => x.OfType(types.ToArray(y => (string)y))) ?? Array.Empty<ContactInfo>();
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
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this instance.</returns>
        public override string ToString()
        {
            var TempName = FullName;
            return string.IsNullOrEmpty(TempName) ? "New User" : TempName;
        }
    }
}