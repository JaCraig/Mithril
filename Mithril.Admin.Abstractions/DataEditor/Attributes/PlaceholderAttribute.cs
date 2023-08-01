namespace Mithril.Admin.Abstractions.DataEditor.Attributes
{
    /// <summary>
    /// Placeholder attribute
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PlaceholderAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceholderAttribute" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public PlaceholderAttribute(string value)
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