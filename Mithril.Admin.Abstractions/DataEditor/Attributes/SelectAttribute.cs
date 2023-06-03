namespace Mithril.Admin.Abstractions.DataEditor.Attributes
{
    /// <summary>
    /// Determines if the system should treat this as a select
    /// TODO: Add tests
    /// </summary>
    /// <seealso cref="QueryAttribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SelectAttribute : QueryAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectAttribute"/> class.
        /// </summary>
        /// <param name="queryType">Type of the query.</param>
        /// <param name="filter">The filter.</param>
        public SelectAttribute(Type queryType, string filter = "") : base(queryType, filter)
        {
        }
    }
}