using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Mithril.Core.Extensions
{
    /// <summary>
    /// Static HTTP context extensions.
    /// </summary>
    public static class StaticHttpContextExtensions
    {
        /// <summary>
        /// Adds the HTTP context accessor.
        /// </summary>
        /// <param name="services">The services.</param>
        public static IServiceCollection? AddStaticHttpContextAccessor(this IServiceCollection? services) => services?.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        /// <summary>
        /// Uses the static HTTP context.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>The app builder</returns>
        public static IApplicationBuilder? UseStaticHttpContext(this IApplicationBuilder? app)
        {
            IHttpContextAccessor? HttpContextAccessor = app?.ApplicationServices.GetService<IHttpContextAccessor>();
            Abstractions.Mvc.Context.HttpContext.Configure(HttpContextAccessor);
            return app;
        }
    }
}