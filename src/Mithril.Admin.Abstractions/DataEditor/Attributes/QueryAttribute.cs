namespace Mithril.Admin.Abstractions.DataEditor.Attributes
{
    /// <summary>
    /// Query attribute
    /// </summary>
    /// <seealso cref="Attribute" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="QueryAttribute"/> class.
    /// </remarks>
    /// <param name="queryType">Type of the query.</param>
    /// <param name="filter">The filter.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class QueryAttribute(Type queryType, string filter = "") : Attribute
    {
        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>The filter.</value>
        public string Filter { get; set; } = filter;

        /// <summary>
        /// Gets the name of the query.
        /// </summary>
        /// <value>The name of the query.</value>
        public Type QueryType { get; set; } = queryType;
    }
}