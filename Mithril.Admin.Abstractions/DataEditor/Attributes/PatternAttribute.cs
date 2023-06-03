namespace Mithril.Admin.Abstractions.DataEditor.Attributes
{
    /// <summary>
    /// Pattern attribute
    /// TODO: Add tests
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PatternAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PatternAttribute" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public PatternAttribute(string value)
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