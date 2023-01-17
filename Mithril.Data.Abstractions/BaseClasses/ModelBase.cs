using Mithril.Data.Abstractions.ExtensionMethods;
using Mithril.Data.Abstractions.Interfaces;
using Mithril.Data.Abstractions.Services;
using System.Security.Claims;

namespace Mithril.Data.Abstractions.BaseClasses
{
    /// <summary>
    /// Model base
    /// </summary>
    /// <typeparam name="TClass">The type of the class.</typeparam>
    /// <seealso cref="IModel"/>
    /// <seealso cref="IComparable"/>
    /// <seealso cref="IComparable{TClass}"/>
    public abstract class ModelBase<TClass> : IModel<TClass>, IComparable, IComparable<TClass>, IEquatable<ModelBase<TClass>>
        where TClass : ModelBase<TClass>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelBase{TClass}"/> class.
        /// </summary>
        protected ModelBase()
        {
            Active = true;
            DateCreated = DateTime.UtcNow;
            DateModified = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this is active.
        /// </summary>
        /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the creator.
        /// </summary>
        /// <value>The creator.</value>
        public virtual IUser? Creator { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>The date created.</value>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>The date modified.</value>
        public DateTime DateModified { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public long ID { get; set; }

        /// <summary>
        /// Gets or sets the modifier.
        /// </summary>
        /// <value>The modifier.</value>
        public virtual IUser? Modifier { get; set; }

        /// <summary>
        /// Gets or sets the tenant associated with the object.
        /// </summary>
        /// <value>The tenant associated with the object.</value>
        public long TenantID { get; set; }

        /// <summary>
        /// Gets all entries.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <returns>All entries.</returns>
        public static IEnumerable<TClass> All(IDataService? dataService) => Query(dataService)?.ToList() ?? Enumerable.Empty<TClass>();

        /// <summary>
        /// Gets all active entries.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <returns>The active entries.</returns>
        public static IEnumerable<TClass> AllActive(IDataService? dataService) => Query(dataService)?.Where(x => x.Active).ToList() ?? Enumerable.Empty<TClass>();

        /// <summary>
        /// Gets all inactive entries.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <returns>The inactive entries.</returns>
        public static IEnumerable<TClass> AllInactive(IDataService? dataService) => Query(dataService)?.Where(x => !x.Active).ToList() ?? Enumerable.Empty<TClass>();

        /// <summary>
        /// Loads the item based on the ID
        /// </summary>
        /// <param name="id">ID of the item to load</param>
        /// <param name="dataService">The data service.</param>
        /// <returns>The specified item</returns>
        public static TClass? Load(long id, IDataService? dataService) => Query(dataService)?.Where(x => x.ID == id).FirstOrDefault();

        /// <summary>
        /// != operator
        /// </summary>
        /// <param name="first">First item</param>
        /// <param name="second">Second item</param>
        /// <returns>returns true if they are not equal, false otherwise</returns>
        public static bool operator !=(ModelBase<TClass>? first, ModelBase<TClass>? second)
        {
            return !(first == second);
        }

        /// <summary>
        /// The &lt; operator
        /// </summary>
        /// <param name="first">First item</param>
        /// <param name="second">Second item</param>
        /// <returns>True if the first item is less than the second, false otherwise</returns>
        public static bool operator <(ModelBase<TClass>? first, ModelBase<TClass>? second)
        {
            return first is null ? second is null : first.CompareTo(second) < 0;
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(ModelBase<TClass>? first, ModelBase<TClass>? second)
        {
            return first is null ? second is null : first.CompareTo(second) <= 0;
        }

        /// <summary>
        /// The == operator
        /// </summary>
        /// <param name="first">First item</param>
        /// <param name="second">Second item</param>
        /// <returns>true if the first and second item are the same, false otherwise</returns>
        public static bool operator ==(ModelBase<TClass>? first, ModelBase<TClass>? second)
        {
            return ReferenceEquals(first, second)
                || (first is not null
                    && second is not null
                    && first.CompareTo(second) == 0);
        }

        /// <summary>
        /// The &gt; operator
        /// </summary>
        /// <param name="first">First item</param>
        /// <param name="second">Second item</param>
        /// <returns>True if the first item is greater than the second, false otherwise</returns>
        public static bool operator >(ModelBase<TClass>? first, ModelBase<TClass>? second)
        {
            return first is null ? second is null : first.CompareTo(second) > 0;
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(ModelBase<TClass>? first, ModelBase<TClass>? second)
        {
            return first is null ? second is null : first.CompareTo(second) >= 0;
        }

        /// <summary>
        /// Queries this instance.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The IQueryable</returns>
        public static IQueryable<TClass>? Query(IDataService? context) => context?.Query<TClass>();

        /// <summary>
        /// Determines whether this instance [can be modified by] the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// <c>true</c> if this instance [can be modified by] the specified user; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool CanBeModifiedBy(ClaimsPrincipal? user) => TenantID == 0 || (user?.HasClaim("Tennant", TenantID.ToString()) ?? true);

        /// <summary>
        /// Determines whether this instance [can be viewed by] the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// <c>true</c> if this instance [can be viewed by] the specified user; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool CanBeViewedBy(ClaimsPrincipal? user) => TenantID == 0 || (user?.HasClaim("Tennant", TenantID.ToString()) ?? true);

        /// <summary>
        /// Compares the object to another object
        /// </summary>
        /// <param name="obj">Object to compare to</param>
        /// <returns>0 if they are equal, -1 if this is smaller, 1 if it is larger</returns>
        public int CompareTo(object? obj) => obj is TClass modelBase ? CompareTo(modelBase) : -1;

        /// <summary>
        /// Compares the object to another object
        /// </summary>
        /// <param name="other">Object to compare to</param>
        /// <returns>0 if they are equal, -1 if this is smaller, 1 if it is larger</returns>
        public virtual int CompareTo(TClass? other) => other is null ? 1 : ReferenceEquals(this, other) ? 0 : ID != 0 ? other.ID.CompareTo(ID) : 1;

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="currentUser">The current user.</param>
        /// <param name="softDelete">if set to <c>true</c> [soft delete].</param>
        /// <returns>The async task.</returns>
        public Task DeleteAsync(IDataService? dataService, ClaimsPrincipal? currentUser, bool softDelete = true)
        {
            if (dataService is null)
                return Task.CompletedTask;
            if (softDelete)
            {
                Active = false;
                return SaveAsync(dataService, currentUser);
            }
            return dataService.DeleteAsync((TClass)this);
        }

        /// <summary>
        /// Determines if two items are equal
        /// </summary>
        /// <param name="obj">The object to compare this to</param>
        /// <returns>true if they are the same, false otherwise</returns>
        public override bool Equals(object? obj) => (obj is ModelBase<TClass> TempObject) && Equals(TempObject);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(IModel? other) => (other is ModelBase<TClass> TempObject) && Equals(TempObject);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(IModel<TClass>? other) => (other is ModelBase<TClass> TempObject) && Equals(TempObject);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(ModelBase<TClass>? other) => other is not null && CompareTo(other) == 0;

        /// <summary>
        /// Returns the hash of this item
        /// </summary>
        /// <returns>the int hash of the item</returns>
        public override int GetHashCode() => HashCode.Combine(ID);

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="currentUser">The current user.</param>
        /// <returns>This.</returns>
        public Task SaveAsync(IDataService? dataService, ClaimsPrincipal? currentUser) => dataService?.SaveAsync(SetupObjectAndReturn(dataService, currentUser)) ?? Task.CompletedTask;

        /// <summary>
        /// Sets up the object.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="currentUser">The current user.</param>
        public virtual void SetupObject(IDataService? dataService, ClaimsPrincipal? currentUser)
        {
            var CurrentUserName = currentUser?.GetName() ?? "system_account";
            SetupObject(dataService, dataService?.Query<IUser>()?.Where(x => x.UserName == CurrentUserName).FirstOrDefault());
        }

        /// <summary>
        /// Sets up the object.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="currentUser">The current user.</param>
        public virtual void SetupObject(IDataService? dataService, IUser? currentUser)
        {
            DateModified = DateTime.UtcNow;
            Modifier = currentUser ?? Modifier;
            Creator ??= Modifier;
            Modifier ??= Creator;
            if (TenantID == 0)
                TenantID = Creator?.TenantID ?? 0;
        }

        /// <summary>
        /// Sets up the object for saving purposes
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="currentUser">The current user.</param>
        /// <returns>This.</returns>
        public virtual TClass SetupObjectAndReturn(IDataService? dataService, ClaimsPrincipal? currentUser)
        {
            SetupObject(dataService, currentUser);
            return (TClass)this;
        }
    }
}