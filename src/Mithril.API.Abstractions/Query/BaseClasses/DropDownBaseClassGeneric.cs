using Inflatable;
using Mithril.API.Abstractions.Query.Interfaces;
using Mithril.API.Abstractions.Query.ViewModels;
using Mithril.Data.Abstractions.Interfaces;
using System.Security.Claims;

namespace Mithril.API.Abstractions.Query.BaseClasses
{
    /// <summary>
    /// Model query base class
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public abstract class DropDownBaseClass<TModel> : IDropDownQuery
        where TModel : class, IModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DropDownBaseClass"/> class.
        /// </summary>
        protected DropDownBaseClass()
        {
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; } = typeof(TModel).Name;

        /// <summary>
        /// Determines whether this instance can run the specified data type.
        /// </summary>
        /// <param name="dataType">Type of the data.</param>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <c>true</c> if this instance can run the specified data type; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool CanRun(string? dataType, ClaimsPrincipal? user) => string.Equals(dataType, Name, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>
        /// The drop down data.
        /// </returns>
        public Task<IEnumerable<DropDownVM<long>>> GetDataAsync(string? filter) => Task.FromResult<IEnumerable<DropDownVM<long>>>(GetValues(filter).Select(x => new DropDownVM<long>(GetKey(x), x.ToString())).OrderBy(x => x.Value));

        /// <summary>
        /// Gets all values that start with the value sent in.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="count">The count.</param>
        /// <returns>The values</returns>
        public IEnumerable<TModel> GetValues(string? value, int count = -1)
        {
            IQueryable<TModel> Query = DbContext<TModel>.CreateQuery();
            if (count > 0)
                Query = Query.Take(count);
            if (!string.IsNullOrEmpty(value))
                Query = FilterQuery(Query, value);
            return Query.ToList();
        }

        /// <summary>
        /// Filters the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="value">The value.</param>
        /// <returns>The filtered query</returns>
        protected abstract IQueryable<TModel> FilterQuery(IQueryable<TModel> query, string value);

        /// <summary>
        /// Gets the key for the dropdown.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The key.</returns>
        protected virtual long GetKey(TModel model) => model.ID;
    }
}