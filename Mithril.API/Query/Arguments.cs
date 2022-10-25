namespace Mithril.API.Query
{
    /// <summary>
    /// Arguments
    /// </summary>
    /// <seealso cref="Dictionary&lt;string, object&gt;"/>
    public class Arguments : Dictionary<string, object?>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Arguments"/> class.
        /// </summary>
        public Arguments()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>The value specified.</returns>
        public TValue? GetValue<TValue>(string key)
        {
            if (!TryGetValue(key, out var value))
                return default;
            return (TValue?)value ?? default;
        }

        /// <summary>
        /// Tries to get the value.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>True if it is found, false otherwise.</returns>
        public bool TryGetValue<TValue>(string key, out TValue? value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                value = default;
                return false;
            }
            value = GetValue<TValue>(key);
            return true;
        }
    }
}