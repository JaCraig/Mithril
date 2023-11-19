namespace Mithril.API.Abstractions.Query.ViewModels
{
    /// <summary>
    /// Paged data view model
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>
    /// <remarks>
    /// Initializes a new instance of the <see cref="PagedDataVM{TData}"/> class.
    /// </remarks>
    /// <param name="page">The page.</param>
    /// <param name="pageSize">Size of the page.</param>
    /// <param name="total">The total.</param>
    /// <param name="session">The session.</param>
    /// <param name="values">The values.</param>
    public class PagedDataVM<TData>(int page, int pageSize, int total, int session, List<TData> values)
    {
        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>The page.</value>
        public int Page { get; set; } = page;

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize { get; set; } = pageSize;

        /// <summary>
        /// Gets or sets the session.
        /// </summary>
        /// <value>The session.</value>
        public int Session { get; set; } = session;

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public int Total { get; set; } = total;

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
        public List<TData>? Values { get; set; } = values ?? [];
    }
}