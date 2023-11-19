namespace Mithril.Admin.Abstractions.DataEditor.Attributes
{
    /// <summary>
    /// Order attribute
    /// </summary>
    /// <seealso cref="Attribute"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="OrderAttribute"/> class.
    /// </remarks>
    /// <param name="order">The order.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class OrderAttribute(int order) : Attribute
    {
        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; } = order;
    }
}