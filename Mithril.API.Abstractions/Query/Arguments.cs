using ObjectCartographer;

namespace Mithril.API.Abstractions.Query
{
    /// <summary>
    /// Arguments
    /// </summary>
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
        public TValue? GetValue<TValue>(string? key)
        {
            TryGetValue(key, out TValue? value);
            return value;
        }

        /// <summary>
        /// Tries to get the value.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>True if it is found, false otherwise.</returns>
        public bool TryGetValue<TValue>(string? key, out TValue? value)
        {
            if (string.IsNullOrWhiteSpace(key) || !TryGetValue(key, out var value2))
            {
                value = default;
                return false;
            }
            value = value2.To<TValue>();
            return true;
        }
    }
}