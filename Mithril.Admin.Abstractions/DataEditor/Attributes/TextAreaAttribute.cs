namespace Mithril.Admin.Abstractions.DataEditor.Attributes
{
    /// <summary>
    /// Determines if the system should treat this as a text area
    /// TODO: Add tests
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class TextAreaAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextAreaAttribute"/> class.
        /// </summary>
        /// <param name="rows">The rows.</param>
        public TextAreaAttribute(int rows = 3)
        {
            Rows = rows;
        }

        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        public int Rows { get; set; }
    }
}