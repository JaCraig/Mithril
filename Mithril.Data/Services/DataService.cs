using BigBook;
using Inflatable;
using Mithril.Data.Abstractions.Interfaces;
using Mithril.Data.Abstractions.Services;
using Mithril.Security.Abstractions;
using System.Security.Claims;

namespace Mithril.Data.Services
{
    /// <summary>
    /// Data service
    /// </summary>
    /// <seealso cref="IDataService"/>
    public class DataService : IDataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataService"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="systemAccounts">The system accounts.</param>
        public DataService(DbContext? dbContext, SystemAccounts systemAccounts)
        {
            DbContext = dbContext;
            SystemAccounts = systemAccounts;
        }

        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>The database context.</value>
        private DbContext? DbContext { get; }

        /// <summary>
        /// Gets the system accounts.
        /// </summary>
        /// <value>The system accounts.</value>
        private SystemAccounts SystemAccounts { get; }

        /// <summary>
        /// Deletes the objects asynchronously.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <param name="user">The user.</param>
        /// <param name="data">The data.</param>
        /// <returns>The number of objects updated.</returns>
        public Task<int> DeleteAsync<TData>(ClaimsPrincipal? user, params TData[] data)
            where TData : class, IModel
        {
            if (data is null || data.Length == 0)
                return Task.FromResult(0);
            return DbContext?.Delete(data.Where(x => x.CanBeModifiedBy(user ?? SystemAccounts.SystemClaimsPrincipal)).ToArray()).ExecuteAsync() ?? Task.FromResult(0);
        }

        /// <summary>
        /// Creates a query used to get information.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <returns>The IQueryable object.</returns>
        public IQueryable<TData>? Query<TData>()
            where TData : class
        {
            return DbContext<TData>.CreateQuery();
        }

        /// <summary>
        /// Saves the objects asynchronously.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <param name="user">The user.</param>
        /// <param name="data">The data.</param>
        /// <returns>The number of objects updated.</returns>
        public Task<int> SaveAsync<TData>(ClaimsPrincipal? user, params TData?[] data)
            where TData : class, IModel
        {
            if (data is null || data.Length == 0)
                return Task.FromResult(0);
            user ??= SystemAccounts.SystemClaimsPrincipal;
            return DbContext?.Save(FilterData(user, data).ToArray()).ExecuteAsync() ?? Task.FromResult(0);
        }

        /// <summary>
        /// Filters the data based on what the user can modify.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <param name="user">The user.</param>
        /// <param name="data">The data.</param>
        /// <returns>The filtered data.</returns>
        private IEnumerable<TData> FilterData<TData>(ClaimsPrincipal? user, params TData?[] data)
            where TData : class, IModel
        {
            foreach (var Item in data)
            {
                if (Item?.CanBeModifiedBy(user) != true)
                    continue;
                Item.SetupObject(this, user);
                yield return Item;
            }
        }
    }
}