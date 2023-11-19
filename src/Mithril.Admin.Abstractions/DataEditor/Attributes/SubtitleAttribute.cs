namespace Mithril.Admin.Abstractions.DataEditor.Attributes
{
    /// <summary>
    /// Subtitle attribute
    /// </summary>
    /// <seealso cref="Attribute" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="SubtitleAttribute" /> class.
    /// </remarks>
    /// <param name="value">The value.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SubtitleAttribute(string value) : Attribute
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; } = value ?? "";
    }
}