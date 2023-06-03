namespace Mithril.Admin.Abstractions.DataEditor.Attributes
{
    /// <summary>
    /// Query attribute
    /// TODO: Add tests
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class QueryAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryAttribute"/> class.
        /// </summary>
        /// <param name="queryType">Type of the query.</param>
        /// <param name="filter">The filter.</param>
        public QueryAttribute(Type queryType, string filter = "")
        {
            QueryType = queryType;
            Filter = filter;
        }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>The filter.</value>
        public string Filter { get; set; }

        /// <summary>
        /// Gets the name of the query.
        /// </summary>
        /// <value>The name of the query.</value>
        public Type QueryType { get; set; }
    }
}