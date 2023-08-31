using BigBook;
using Inflatable;
using Mithril.Data.Abstractions.Interfaces;
using Mithril.Data.Abstractions.Services;
using Mithril.Security.Abstractions;
using System.Data;
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
            return data is null || data.Length == 0
                ? Task.FromResult(0)
                : DbContext?.Delete(data.Where(x => x.CanBeModifiedBy(user ?? SystemAccounts.SystemClaimsPrincipal)).ToArray()).ExecuteAsync() ?? Task.FromResult(0);
        }

        /// <summary>
        /// Creates a query used to get information.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <returns>The IQueryable object.</returns>
        public IQueryable<TData>? Query<TData>()
            where TData : class => DbContext<TData>.CreateQuery();

        /// <summary>
        /// Runs a scalar query and returns data of the specific type.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The resulting data.
        /// </returns>
        public Task<TData> QueryScalarAsync<TData>(string query, CommandType commandType, string connection, params object[] parameters)
            where TData : class => DbContext<TData>.ExecuteScalarAsync(query, commandType, connection, parameters ?? Array.Empty<object>());

        /// <summary>
        /// Runs a query and returns data of the specific type.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The resulting data.
        /// </returns>
        public Task<IEnumerable<TData>> QueryAsync<TData>(string query, CommandType commandType, string connection, params object[] parameters)
            where TData : class => DbContext<TData>.ExecuteAsync(query, commandType, connection, parameters ?? Array.Empty<object>());

        /// <summary>
        /// Runs a dynamic query and returns the results.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The data resulting from the query.</returns>
        public Task<IEnumerable<dynamic>> QueryDynamicAsync(string query, CommandType commandType, string connection, params object[] parameters) => DbContext.ExecuteAsync(query, commandType, connection, parameters ?? Array.Empty<object>());

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
            foreach (TData? Item in data)
            {
                if (Item?.CanBeModifiedBy(user) != true)
                    continue;
                Item.SetupObject(this, user);
                yield return Item;
            }
        }
    }
}