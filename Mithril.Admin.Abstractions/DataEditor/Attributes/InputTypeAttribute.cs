namespace Mithril.Admin.Abstractions.DataEditor.Attributes
{
    /// <summary>
    /// Input type attribute
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class InputTypeAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputTypeAttribute"/> class.
        /// </summary>
        /// <param name="inputType">Type of the input.</param>
        public InputTypeAttribute(string inputType)
        {
            InputType = inputType;
        }

        /// <summary>
        /// Gets the type of the input.
        /// </summary>
        /// <value>
        /// The type of the input.
        /// </value>
        public string InputType { get; }
    }
}