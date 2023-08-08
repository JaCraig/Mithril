namespace Mithril.API.Abstractions.Query.ViewModels
{
    /// <summary>
    /// Drop down VM
    /// </summary>
    public class DropDownVM<TKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DropDownVM{TKey}"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public DropDownVM(TKey key, string? value)
        {
            Key = key;
            Value = value ?? "";
        }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public TKey Key { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string? Value { get; set; }
    }
}