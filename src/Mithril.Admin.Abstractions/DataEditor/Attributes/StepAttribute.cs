namespace Mithril.Admin.Abstractions.DataEditor.Attributes
{
    /// <summary>
    /// Step attribute
    /// </summary>
    /// <seealso cref="Attribute" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="StepAttribute"/> class.
    /// </remarks>
    /// <param name="value">The value.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class StepAttribute(decimal value) : Attribute
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public decimal Value { get; set; } = value;
    }
}