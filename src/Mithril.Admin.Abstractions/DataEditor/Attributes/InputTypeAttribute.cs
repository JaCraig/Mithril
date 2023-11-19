namespace Mithril.Admin.Abstractions.DataEditor.Attributes
{
    /// <summary>
    /// Input type attribute
    /// </summary>
    /// <seealso cref="Attribute" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="InputTypeAttribute"/> class.
    /// </remarks>
    /// <param name="inputType">Type of the input.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class InputTypeAttribute(string inputType) : Attribute
    {
        /// <summary>
        /// Gets the type of the input.
        /// </summary>
        /// <value>
        /// The type of the input.
        /// </value>
        public string InputType { get; } = inputType;
    }
}