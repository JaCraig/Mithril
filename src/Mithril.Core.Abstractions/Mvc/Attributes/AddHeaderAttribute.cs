using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace Mithril.Core.Abstractions.Mvc.Attributes
{
    /// <summary>
    /// Add Header Attribute
    /// </summary>
    /// <seealso cref="ActionFilterAttribute"/>
    public sealed class AddHeaderAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddHeaderAttribute"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="values">The values.</param>
        public AddHeaderAttribute(string key, string[] values)
        {
            Key = key;
            Values = values;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddHeaderAttribute"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public AddHeaderAttribute(string key, string value)
        {
            Key = key;
            Values = [value];
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        private string Key { get; }

        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <value>The values.</value>
        private string[] Values { get; }

        /// <summary>
        /// On Action Executing.
        /// </summary>
        /// <param name="context">Context</param>
        /// <inheritdoc/>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context is null)
                return;
            Microsoft.AspNetCore.Http.IHeaderDictionary Headers = context.HttpContext.Response.Headers;
            if (Headers.ContainsKey(Key))
                Headers[Key] = Values;
            else
                Headers.Append(new KeyValuePair<string, StringValues>(Key, Values));
        }
    }
}