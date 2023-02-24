using Mithril.Data.Abstractions.Interfaces;
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
        Task<int> SaveAsync<TData>(ClaimsPrincipal? user, params TData[] data)
            where TData : class, IModel;
    }
}