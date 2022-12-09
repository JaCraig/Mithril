using BigBook;
using System.Text.RegularExpressions;

namespace Mithril.Core.Abstractions.Services.Options
{
    /// <summary>
    /// IP Filter policy
    /// </summary>
    public class IPFilterPolicy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IPFilterPolicy"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public IPFilterPolicy(string? name)
        {
            Name = name ?? "Default";
        }

        /// <summary>
        /// Gets or sets the black list.
        /// </summary>
        /// <value>The black list.</value>
        public string? BlackList { get; private set; } = "";

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the white list.
        /// </summary>
        /// <value>The white list.</value>
        public string? WhiteList { get; private set; } = "";

        /// <summary>
        /// Gets or sets the black list filters.
        /// </summary>
        /// <value>The black list filters.</value>
        private Regex[]? BlackListFilters { get; set; }

        /// <summary>
        /// Gets or sets the white list filters.
        /// </summary>
        /// <value>The white list filters.</value>
        private Regex[]? WhiteListFilters { get; set; }

        /// <summary>
        /// Determines whether the specified ip address is allowed.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns><c>true</c> if the specified ip address is allowed; otherwise, <c>false</c>.</returns>
        public bool IsAllowed(string ipAddress)
        {
            return !(BlackListFilters?.Any(x => x.IsMatch(ipAddress)) ?? false)
                && (WhiteListFilters?.Any(x => x.IsMatch(ipAddress)) ?? true);
        }

        /// <summary>
        /// Sets the black list for this policy.
        /// </summary>
        /// <param name="blackList">The black list.</param>
        /// <returns>This.</returns>
        public IPFilterPolicy SetBlackList(string? blackList)
        {
            BlackList = blackList ?? "";
            BlackListFilters = BlackList.Split(';', StringSplitOptions.RemoveEmptyEntries).ToArray(x => new Regex(x, RegexOptions.IgnoreCase | RegexOptions.Compiled));
            return this;
        }

        /// <summary>
        /// Sets the white list for this policy.
        /// </summary>
        /// <param name="whiteList">The white list.</param>
        /// <returns>This.</returns>
        public IPFilterPolicy SetWhiteList(string? whiteList)
        {
            WhiteList = whiteList ?? "";
            WhiteListFilters = WhiteList.Split(';', StringSplitOptions.RemoveEmptyEntries).ToArray(x => new Regex(x, RegexOptions.IgnoreCase | RegexOptions.Compiled));
            return this;
        }
    }
}