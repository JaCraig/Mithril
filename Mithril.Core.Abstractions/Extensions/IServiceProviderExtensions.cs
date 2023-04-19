using Microsoft.Extensions.DependencyInjection;

namespace Mithril.Core.Abstractions.Extensions
{
    /// <summary>
    /// Service provider extensions
    /// </summary>
    public static class IServiceProviderExtensions
    {
        /// <summary>
        /// Determines if the service has been registered.
        /// </summary>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <param name="services">The service provider.</param>
        /// <returns>True if it exists, false otherwise.</returns>
        public static bool Exists<TClass>(this IServiceProvider? services)
        {
            return services is not null && services.GetService<TClass>() != null;
        }
    }
}