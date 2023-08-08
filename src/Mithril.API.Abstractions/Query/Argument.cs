using Mithril.API.Abstractions.Query.Interfaces;

namespace Mithril.API.Abstractions.Query
{
    /// <summary>
    /// Argument that the query accepts
    /// </summary>
    /// <typeparam name="TValue">The type of the value expected.</typeparam>
    /// <seealso cref="IArgument"/>
    public class Argument<TValue> : IArgument
    {
        /// <summary>
        /// Gets the type of the argument.
        /// </summary>
        /// <value>The type of the argument.</value>
        public Type ArgumentType => typeof(TValue);

        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        /// <value>The default value.</value>
        public object? DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string? Name { get; set; }

        /// <summary>
        /// To string
        /// </summary>
        private string _ToString = "";

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(_ToString))
                return _ToString;
            _ToString = Name ?? "";
            if (DefaultValue is not null)
                _ToString += " = " + DefaultValue.ToString();
            return _ToString;
        }
    }
}