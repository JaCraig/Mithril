namespace Mithril.Admin.Abstractions.DataEditor.Attributes
{
    /// <summary>
    /// Determines if the system should treat this as a text area
    /// </summary>
    /// <seealso cref="System.Attribute" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="TextAreaAttribute"/> class.
    /// </remarks>
    /// <param name="rows">The rows.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class TextAreaAttribute(int rows = 3) : Attribute
    {
        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        public int Rows { get; set; } = rows;
    }
}