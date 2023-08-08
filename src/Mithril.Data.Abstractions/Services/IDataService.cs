using Mithril.Data.Abstractions.Interfaces;
using System.Data;
using System.Security.Claims;

namespace Mithril.Data.Abstractions.Services
{
    /// <summary>
    /// Data service interface
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Deletes the objects asynchronously.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <param name="user">The user.</param>
        /// <param name="data">The data.</param>
        /// <returns>The number of objects updated.</returns>
        Task<int> DeleteAsync<TData>(ClaimsPrincipal? user, params TData[] data)
             where TData : class, IModel;

        /// <summary>
        /// Creates a query used to get information.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <returns>The IQueryable object.</returns>
        IQueryable<TData>? Query<TData>()
            where TData : class;

        /// <summary>
        /// Saves the object asynchronously.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <param name="user">The user.</param>
        /// <param name="data">The data.</param>
        /// <returns>The number of objects updated.</returns>
        Task<int> SaveAsync<TData>(ClaimsPrincipal? user, params TData?[] data)
            where TData : class, IModel;

        /// <summary>
        /// Runs a scalar query and returns data of the specific type.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The resulting data.</returns>
        Task<TData> QueryScalarAsync<TData>(string query, CommandType commandType, string connection, params object[] parameters)
            where TData : class;

        /// <summary>
        /// Runs a query and returns data of the specific type.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The resulting data.</returns>
        Task<IEnumerable<TData>> QueryAsync<TData>(string query, CommandType commandType, string connection, params object[] parameters)
            where TData : class;

        /// <summary>
        /// Runs a dynamic query and returns the results.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The data resulting from the query.</returns>
        Task<IEnumerable<dynamic>> QueryDynamicAsync(string query, CommandType commandType, string connection, params object[] parameters);
    }
}