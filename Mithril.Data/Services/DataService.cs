using Inflatable;
using Mithril.Data.Abstractions.Services;

namespace Mithril.Data.Inflatable.Services
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
        public DataService(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>The database context.</value>
        public DbContext DbContext { get; }

        /// <summary>
        /// Deletes the objects asynchronously.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <param name="data">The data.</param>
        /// <returns>The number of objects updated.</returns>
        public Task<int> DeleteAsync<TData>(params TData[] data)
            where TData : class
        {
            return DbContext.Delete(data).ExecuteAsync();
        }

        /// <summary>
        /// Creates a query used to get information.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <returns>The IQueryable object.</returns>
        public IQueryable<TData> Query<TData>()
            where TData : class
        {
            return DbContext<TData>.CreateQuery();
        }

        /// <summary>
        /// Saves the objects asynchronously.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <param name="data">The data.</param>
        /// <returns>The number of objects updated.</returns>
        public Task<int> SaveAsync<TData>(params TData[] data)
            where TData : class
        {
            return DbContext.Save(data).ExecuteAsync();
        }
    }
}