namespace Mithril.Admin.Abstractions.DataEditor.Attributes
{
    /// <summary>
    /// Order attribute
    /// </summary>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class OrderAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderAttribute"/> class.
        /// </summary>
        /// <param name="order">The order.</param>
        public OrderAttribute(int order)
        {
            Order = order;
        }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; }
    }
}