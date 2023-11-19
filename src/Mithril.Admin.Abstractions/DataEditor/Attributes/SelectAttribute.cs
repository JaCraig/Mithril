namespace Mithril.Admin.Abstractions.DataEditor.Attributes
{
    /// <summary>
    /// Determines if the system should treat this as a select
    /// </summary>
    /// <seealso cref="QueryAttribute" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="SelectAttribute"/> class.
    /// </remarks>
    /// <param name="queryType">Type of the query.</param>
    /// <param name="filter">The filter.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SelectAttribute(Type queryType, string filter = "") : QueryAttribute(queryType, filter)
    {
    }
}