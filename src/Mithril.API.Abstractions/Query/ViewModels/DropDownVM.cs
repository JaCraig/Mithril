namespace Mithril.API.Abstractions.Query.ViewModels
{
    /// <summary>
    /// Drop down VM
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="DropDownVM{TKey}"/> class.
    /// </remarks>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    public class DropDownVM<TKey>(TKey key, string? value)
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public TKey Key { get; set; } = key;

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string? Value { get; set; } = value ?? "";
    }
}