namespace Mithril.Admin.Abstractions.DataEditor.Attributes
{
    /// <summary>
    /// Step attribute
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class StepAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StepAttribute"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public StepAttribute(decimal value)
        { Value = value; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public decimal Value { get; set; }
    }
}