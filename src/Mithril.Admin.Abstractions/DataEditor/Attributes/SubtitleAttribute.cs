namespace Mithril.Admin.Abstractions.DataEditor.Attributes
{
    /// <summary>
    /// Subtitle attribute
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SubtitleAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubtitleAttribute" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public SubtitleAttribute(string value)
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