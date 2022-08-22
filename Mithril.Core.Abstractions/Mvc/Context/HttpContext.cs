using Microsoft.AspNetCore.Http;

namespace Mithril.Core.Abstractions.Mvc.Context
{
    /// <summary>
    /// Static http context
    /// </summary>
    public static class HttpContext
    {
        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>The current.</value>
        public static Microsoft.AspNetCore.Http.HttpContext? Current => _contextAccessor?.HttpContext;

        /// <summary>
        /// The context accessor
        /// </summary>
        private static IHttpContextAccessor? _contextAccessor;

        /// <summary>
        /// Configures the specified context accessor.
        /// </summary>
        /// <param name="contextAccessor">The context accessor.</param>
        public static void Configure(IHttpContextAccessor? contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
    }
}