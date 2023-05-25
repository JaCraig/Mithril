namespace Mithril.API.Abstractions.Query.ViewModels
{
    /// <summary>
    /// Paged data view model
    /// TODO: Add tests
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>
    public class PagedDataVM<TData>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagedDataVM{TData}"/> class.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="total">The total.</param>
        /// <param name="session">The session.</param>
        /// <param name="values">The values.</param>
        public PagedDataVM(int page, int pageSize, int total, int session, List<TData> values)
        {
            Page = page;
            PageSize = pageSize;
            Total = total;
            Session = session;
            Values = values ?? new List<TData>();
        }

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>The page.</value>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the session.
        /// </summary>
        /// <value>The session.</value>
        public int Session { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
        public List<TData>? Values { get; set; }
    }
}