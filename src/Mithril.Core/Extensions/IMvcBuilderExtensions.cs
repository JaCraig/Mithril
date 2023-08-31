using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;

namespace Mithril.Core.Extensions
{
    /// <summary>
    /// IMvcBuilder extensions
    /// </summary>
    public static class IMvcBuilderExtensions
    {
        /// <summary>
        /// Adds the CSP media type.
        /// </summary>
        /// <param name="mvcBuilder">The MVC builder.</param>
        /// <returns>The MVC builder</returns>
        public static IMvcBuilder? AddCspMediaType(this IMvcBuilder? mvcBuilder)
        {
            return mvcBuilder?.AddMvcOptions(options =>
            {
                options.InputFormatters
                        .Where(item => item.GetType() == typeof(SystemTextJsonInputFormatter))
                        .Cast<SystemTextJsonInputFormatter>()
                        .FirstOrDefault()
                        ?.SupportedMediaTypes
                        .Add("application/csp-report");
            });
        }
    }
}