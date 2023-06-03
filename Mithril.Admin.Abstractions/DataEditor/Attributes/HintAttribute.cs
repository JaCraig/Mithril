namespace Mithril.Admin.Abstractions.DataEditor.Attributes
{
    /// <summary>
    /// Hint attribute
    /// TODO: Add tests
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class HintAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HintAttribute" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public HintAttribute(string value)
        {
            Value = value ?? "";
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }
    }
}