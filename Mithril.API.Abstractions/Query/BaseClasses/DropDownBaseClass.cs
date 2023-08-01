using Mithril.API.Abstractions.Query.Interfaces;
using Mithril.API.Abstractions.Query.ViewModels;
using SQLHelperDB;
using System.Security.Claims;

namespace Mithril.API.Abstractions.Query.BaseClasses
{
    /// <summary>
    /// Drop down base class
    /// </summary>
    /// <seealso cref="IDropDownQuery"/>
    public abstract class DropDownBaseClass : IDropDownQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DropDownBaseClass"/> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        protected DropDownBaseClass(SQLHelper helper)
        {
            _Helper = helper;
        }

        /// <summary>
        /// Gets the helper.
        /// </summary>
        /// <value>The helper.</value>
        private readonly SQLHelper _Helper;

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the helper.
        /// </summary>
        /// <value>The helper.</value>
        protected SQLHelper Helper => _Helper.Copy();

        /// <summary>
        /// Determines whether this instance can run the specified data type.
        /// </summary>
        /// <param name="dataType">Type of the data.</param>
        /// <param name="user">The user.</param>
        /// <returns>
        /// <c>true</c> if this instance can run the specified data type; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool CanRun(string? dataType, ClaimsPrincipal? user)
        {
            return string.Equals(dataType, Name, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The drop down data.</returns>
        public abstract Task<IEnumerable<DropDownVM<long>>> GetDataAsync(string? filter);
    }
}