using Mithril.API.Abstractions.Query.Interfaces;
using Mithril.API.Abstractions.Query.ViewModels;
using Mithril.Data.Abstractions.Services;
using SQLHelperDB;
using System.Data;
using System.Security.Claims;

namespace Mithril.Security.Admin.DropDowns
{
    /// <summary>
    /// Claim Types Drop Down
    /// </summary>
    /// <seealso cref="IDropDownQuery" />
    public class ClaimTypesDropDown : IDropDownQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimTypesDropDown"/> class.
        /// </summary>
        /// <param name="sQLHelper">The s ql helper.</param>
        public ClaimTypesDropDown(SQLHelper? sQLHelper)
        {
            Helper = sQLHelper;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; } = "ClaimTypes";

        /// <summary>
        /// Gets the helper.
        /// </summary>
        /// <value>
        /// The helper.
        /// </value>
        private SQLHelper? Helper { get; }

        /// <summary>
        /// Determines whether this instance can run the specified data type.
        /// </summary>
        /// <param name="dataType">Type of the data.</param>
        /// <param name="user">The user.</param>
        /// <returns>
        /// <c>true</c> if this instance can run the specified data type; otherwise, <c>false</c>.
        /// </returns>
        public bool CanRun(string? dataType, ClaimsPrincipal? user)
        {
            return string.Equals(dataType, Name, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Gets the data asynchronously.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>
        /// The drop down items.
        /// </returns>
        public async Task<IEnumerable<DropDownVM<long>>?> GetDataAsync(IDataService? dataService, string? filter)
        {
            if (Helper is null)
                return Enumerable.Empty<DropDownVM<long>>();
            var Values = await (Helper.CreateBatch()
                   .AddQuery(CommandType.Text, "SELECT DISTINCT [Type_] FROM [Mithril].[dbo].[UserClaim_]")
                   .ExecuteAsync())
               .ConfigureAwait(false);
            return Values.FirstOrDefault()?.Select(x => new DropDownVM<long>(0, x.Type_));
        }
    }
}